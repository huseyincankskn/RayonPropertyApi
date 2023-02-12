
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Business.Concrete
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IBlogFileService _blogFileService;

        public BlogService(IBlogRepository blogRepository,
                           IMapper mapper,
                           IBlogCategoryRepository blogCategoryRepository,
                           IBlogFileService blogFileService)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _blogCategoryRepository = blogCategoryRepository;
            _blogFileService = blogFileService;
        }

        public IDataResult<IQueryable<BlogVm>> GetListQueryableOdata()
        {
            var entityList = _blogRepository.GetAllForOdataWithPassive()
                                            .Include(x => x.BlogCategory)
                                            .Include(x => x.BlogFile);
            var vmList = _mapper.ProjectTo<BlogVm>(entityList);
            return new SuccessDataResult<IQueryable<BlogVm>>(vmList);
        }

        public IDataResult<BlogVm> GetById(Guid id)
        {
            var entity = _blogRepository.GetAllForOdataWithPassive()
                                        .Include(x => x.BlogCategory)
                                        .Include(x => x.BlogFile)
                                        .FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return new ErrorDataResult<BlogVm>(Messages.EntityNotFound);
            }
            var vm = _mapper.Map<BlogVm>(entity);
            return new SuccessDataResult<BlogVm>(vm);
        }


        [ValidationAspect(typeof(BlogAddValidation))]
        public IDataResult<Blog> Add(IFormFile file, BlogAddDto dto)
        {
            var blog = _blogRepository.GetAllForOdata().FirstOrDefault(x => x.Title == dto.Title);
            if (blog != null)
            {
                return new ErrorDataResult<Blog>(Messages.EntityAlreadyExist);
            }

            var blogCategory = _blogCategoryRepository.GetAllForOdata()
                                                      .FirstOrDefault(x => x.Id == dto.BlogCategoryId || x.Name == dto.BlogCategoryName);
            if (blogCategory == null)
            {
                return new ErrorDataResult<Blog>(Messages.BlogCategoryNotFound);
            }

            dto.BlogCategoryId = blogCategory.Id;
            dto.TrimAllProps();
            var blogFileAdd = _blogFileService.SaveImage(file);
            dto.BlogFileId = blogFileAdd.Data.Id;
            var addEntity = _mapper.Map<Blog>(dto);
            _blogRepository.Add(addEntity);
            return new SuccessDataResult<Blog>(addEntity);
        }

        public Core.Utilities.Results.IResult Delete(Guid id)
        {
            var entity = _blogRepository.GetByIdWithPassive(id);
            if (entity == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }
            var modelActive = !entity.IsActive;
            entity.IsActive = modelActive;
            _blogRepository.Update(entity);
            return new SuccessResult(Messages.EntityUpdated);
        }

        [ValidationAspect(typeof(BlogUpdateValidation))]
        public Core.Utilities.Results.IResult Update(IFormFile? file, BlogUpdateDto dto)
        {
            var blog = _blogRepository.GetById(dto.Id);
            if (blog == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }

            var sameEntity = _blogRepository.GetAllForOdata().FirstOrDefault(x => x.Title == dto.Title && x.Id != dto.Id);
            if (sameEntity != null)
            {
                return new ErrorResult(Messages.SameEntity);
            }

            var blogCategory = _blogCategoryRepository.GetAllForOdata().FirstOrDefault(x => x.Id == dto.BlogCategoryId || x.Name == dto.BlogCategoryName);
            if (blogCategory == null)
            {
                return new ErrorDataResult<Blog>(Messages.BlogCategoryNotFound);
            }

            dto.BlogCategoryId = blogCategory.Id;
            dto.TrimAllProps();
            if (file != null)
            {
                var blogFileAdd = _blogFileService.SaveImage(file);
                dto.BlogFileId = blogFileAdd.Data.Id;
            }
            blog = _mapper.Map(dto, blog);
            _blogRepository.Update(blog);
            return new SuccessResult(Messages.EntityUpdated);
        }
    }
}

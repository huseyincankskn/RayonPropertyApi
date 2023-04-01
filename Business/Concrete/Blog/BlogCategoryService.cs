using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.EntityFramework.Repository;
using DataAccess.Concrete.EntityFramework.Repositories;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Enums;
using Entities.VMs;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Concrete
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly ITranslateRepository _translateRepository;

        public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository,
                                   IMapper mapper,
                                   IBlogRepository blogRepository,
                                   ITranslateRepository translateRepository)
        {
            _blogCategoryRepository = blogCategoryRepository;
            _mapper = mapper;
            _blogRepository = blogRepository;
            _translateRepository = translateRepository;
        }

        public IDataResult<IQueryable<BlogCategoryVm>> GetListQueryableOdata()
        {
            var entityList = _blogCategoryRepository.GetAllForOdataWithPassive();
            var vmList = _mapper.ProjectTo<BlogCategoryVm>(entityList);
            return new SuccessDataResult<IQueryable<BlogCategoryVm>>(vmList);
        }

        public IDataResult<BlogCategoryVm> GetById(Guid id)
        {
            var entity = _blogCategoryRepository.GetById(id);
            if (entity == null)
            {
                return new ErrorDataResult<BlogCategoryVm>(Messages.EntityNotFound);
            }
            var vm = _mapper.Map<BlogCategoryVm>(entity);
            return new SuccessDataResult<BlogCategoryVm>(vm);
        }

        [ValidationAspect(typeof(BlogCategoryAddValidation))]
        public IDataResult<BlogCategory> Add(BlogCategoryAddDto dto)
        {
            var sameEntity = _blogCategoryRepository.GetAllForOdata().FirstOrDefault(x => x.Name == dto.Name);
            if (sameEntity != null)
            {
                return new ErrorDataResult<BlogCategory>(Messages.SameEntity);
            }

            dto.TrimAllProps();
            var addEntity = _mapper.Map<BlogCategory>(dto);
            _blogCategoryRepository.Add(addEntity);

            var translate = new Translate()
            {
                Key = addEntity.Name,
                KeyDe = addEntity.NameDe,
                KeyRu = addEntity.NameRu,
            };

            _translateRepository.Add(translate);
            return new SuccessDataResult<BlogCategory>(addEntity);
        }

        public IResult Delete(Guid id)
        {
            var entity = _blogCategoryRepository.GetByIdWithPassive(id);
            if (entity == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }
            var modelActive = !entity.IsActive;
            entity.IsActive = modelActive;
            _blogCategoryRepository.Update(entity);
            return new SuccessResult(Messages.EntityUpdated);
        }

        [ValidationAspect(typeof(BlogCategoryUpdateValidation))]
        public IResult Update(BlogCategoryUpdateDto dto)
        {
            var blogCategory = _blogCategoryRepository.GetById(dto.Id);
            if (blogCategory == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }

            var sameEntity = _blogCategoryRepository.GetAllForOdata().FirstOrDefault(x => x.Name == dto.Name && x.Id != dto.Id);
            if (sameEntity != null)
            {
                return new ErrorResult(Messages.SameEntity);
            }

            dto.TrimAllProps();
            blogCategory = _mapper.Map(dto, blogCategory);
            _blogCategoryRepository.Update(blogCategory);

            var translateEntity = _translateRepository.GetAllForOdata().FirstOrDefault(x => x.Key == blogCategory.Name);
            if (translateEntity != null)
            {
                translateEntity.KeyDe = blogCategory.NameDe;
                translateEntity.KeyRu = blogCategory.NameRu;
                _translateRepository.Update(translateEntity);
            }
            else
            {
                var translate = new Translate()
                {
                    Key = blogCategory.Name,
                    KeyDe = blogCategory.NameDe,
                    KeyRu = blogCategory.NameRu,
                };

                _translateRepository.Add(translate);
            }
            return new SuccessResult(Messages.EntityUpdated);
        }

        public IDataResult<List<BlogCategoryVm>> GetListForWebSite()
        {
            var query = _blogRepository.GetAllForWithoutLogin().Include(x => x.BlogCategory)
              .GroupBy(p => new { p.BlogCategory.Name })
              .Select(g => new BlogCategoryVm
              {
                  Name = g.Key.Name,
                  Count = g.Count(),
              }).ToList();

            if (query.Any())
            {
                var totalVm = new BlogCategoryVm
                {
                    Count = query.Sum(x => x.Count),
                    Name = "Tümü",
                };

                query.Add(totalVm);
                query.Reverse();
            }
            return new SuccessDataResult<List<BlogCategoryVm>>(query);
        }
    }
}

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Concrete
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IMapper _mapper;

        public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository, IMapper mapper)
        {
            _blogCategoryRepository = blogCategoryRepository;
            _mapper = mapper;
        }

        public IDataResult<IQueryable<BlogCategoryVm>> GetListQueryableOdata()
        {
            var entityList = _blogCategoryRepository.GetAllForOdata();
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
            var sameEntity = _blogCategoryRepository.GetAllForOdata().Where(x => x.Name == dto.Name);
            if (sameEntity != null)
            {
                return new ErrorDataResult<BlogCategory>(Messages.SameEntity);
            }

            dto.TrimAllProps();
            var addEntity = _mapper.Map<BlogCategory>(dto);
            _blogCategoryRepository.Add(addEntity);
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
            return new SuccessResult(Messages.EntityUpdated);
        }
    }
}

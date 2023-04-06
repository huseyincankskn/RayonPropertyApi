
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

namespace Business.Concrete
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IBlogFileService _blogFileService;
        private readonly ITranslateRepository _translateRepository;
        private readonly ITranslateService _translateService;

        public BlogService(IBlogRepository blogRepository,
                           IMapper mapper,
                           IBlogCategoryRepository blogCategoryRepository,
                           IBlogFileService blogFileService,
                           ITranslateRepository translateRepository,
                           ITranslateService translateService)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _blogCategoryRepository = blogCategoryRepository;
            _blogFileService = blogFileService;
            _translateRepository = translateRepository;
            _translateService = translateService;
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

            #region Translate
            var addTranslateList = new List<Translate>()
            {
                new Translate()
                {
                    Key = addEntity.Title,
                    KeyDe = addEntity.TitleDe,
                    KeyRu = addEntity.TitleRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                },
                new Translate()
                {
                    Key = addEntity.Post,
                    KeyDe = addEntity.PostDe,
                    KeyRu = addEntity.PostRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                }
            };

            _translateRepository.AddRange(addTranslateList);

            addEntity.TitleTranslateKey = addTranslateList.First().TranslateKey;
            addEntity.PostTranslateKey = addTranslateList.Last().TranslateKey;
            #endregion

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

            #region Translate
            var addTranslateList = new List<Translate>();

            var translateTitle = _translateRepository.GetAllForOdata().FirstOrDefault(x => x.Key == blog.Title
                                                                                            && x.KeyDe == blog.TitleDe
                                                                                            && x.KeyRu == blog.TitleRu);
            if (translateTitle == null)
            {
                var TranslateEntity = new Translate()
                {
                    Key = blog.Title,
                    KeyDe = blog.TitleDe,
                    KeyRu = blog.TitleRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                };
                addTranslateList.Add(TranslateEntity);
                blog.TitleTranslateKey = TranslateEntity.TranslateKey;
            }

            var translatePost = _translateRepository.GetAllForOdata().FirstOrDefault(x => x.Key == blog.Post
                                                                                          && x.KeyDe == blog.PostDe
                                                                                          && x.KeyRu == blog.PostRu);
            if (translatePost == null)
            {
                var TranslateEntity = new Translate()
                {
                    Key = blog.Post,
                    KeyDe = blog.PostDe,
                    KeyRu = blog.PostRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                };
                addTranslateList.Add(TranslateEntity);
                blog.PostTranslateKey = TranslateEntity.TranslateKey;
            }

            if (addTranslateList.Any())
            {
                _translateRepository.AddRange(addTranslateList);
            }

            #endregion

            _blogRepository.Update(blog);
            return new SuccessResult(Messages.EntityUpdated);
        }

        public IDataResult<IQueryable<BlogVm>> GetListForWebSite()
        {
            var entityList = _blogRepository.GetAllForWithoutLogin()
                                .Include(x => x.BlogCategory)
                                .Include(x => x.BlogFile)
                                .OrderByDescending(x => x.AddDate);
            var vmList = _mapper.ProjectTo<BlogVm>(entityList);
            return new SuccessDataResult<IQueryable<BlogVm>>(vmList);
        }

        public IDataResult<BlogVm> GetByIdForWebSite(Guid id)
        {
            var entity = _blogRepository.GetAllForWithoutLogin()
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
    }
}

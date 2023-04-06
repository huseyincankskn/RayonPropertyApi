using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract.EntityFramework.Repository;
using DataAccess.Concrete.EntityFramework.Repositories;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ITranslateRepository _translateRepository;
        private readonly ITranslateService _translateService;

        public CommentService(ICommentRepository commentRepository,
                              IMapper mapper,
                              ITranslateRepository translateRepository,
                              ITranslateService translateService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _translateRepository = translateRepository;
            _translateService = translateService;
        }

        public IDataResult<IQueryable<CommentVm>> GetListQueryableOdata()
        {
            var entityList = _commentRepository.GetAllForOdataWithPassive();
            var vmList = _mapper.ProjectTo<CommentVm>(entityList);
            return new SuccessDataResult<IQueryable<CommentVm>>(vmList);
        }

        public IDataResult<CommentVm> GetById(Guid id)
        {
            var entity = _commentRepository.GetAllForOdataWithPassive()
                            .FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return new ErrorDataResult<CommentVm>(Messages.EntityNotFound);
            }
            var vm = _mapper.Map<CommentVm>(entity);
            return new SuccessDataResult<CommentVm>(vm);
        }


        [ValidationAspect(typeof(CommentAddValidation))]
        public IDataResult<Comment> Add(CommentAddDto dto)
        {
            var sameEntity = _commentRepository.GetAllForOdata().FirstOrDefault(x => x.Name == dto.Name
                                                                                && x.Country == dto.Country
                                                                                && x.CommentText == dto.CommentText);
            if (sameEntity != null)
            {
                return new ErrorDataResult<Comment>(Messages.SameEntity);
            }
            dto.TrimAllProps();
            var addEntity = _mapper.Map<Comment>(dto);
            var translate = new Translate()
            {
                Key = addEntity.CommentText,
                KeyDe = addEntity.CommentTextDe,
                KeyRu = addEntity.CommentTextRu,
                TranslateKey = _translateService.GenerateUniqueTranslateKey()
            };
            addEntity.CommentTextTranslateKey = translate.TranslateKey;
            _translateRepository.Add(translate);
            _commentRepository.Add(addEntity);
            return new SuccessDataResult<Comment>(addEntity);
        }


        [ValidationAspect(typeof(CommentUpdateValidation))]
        public IResult Update(CommentUpdateDto dto)
        {
            var comment = _commentRepository.GetById(dto.Id);
            if (comment == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }
            var sameEntity = _commentRepository.GetAllForOdata().FirstOrDefault(x => x.Name == dto.Name
                                                                                && x.Country == dto.Country
                                                                                && x.CommentText == dto.CommentText
                                                                                && x.Id != dto.Id);
            if (sameEntity != null)
            {
                return new ErrorDataResult<Comment>(Messages.SameEntity);
            }
            dto.TrimAllProps();
            comment = _mapper.Map(dto, comment);


            var translateEntity = _translateRepository.GetAllForOdata().FirstOrDefault(x => x.Key == comment.CommentText
                                                                                 && x.KeyDe == comment.CommentTextDe
                                                                                 && x.KeyRu == comment.CommentTextRu);
            if (translateEntity == null)
            {
                var translate = new Translate()
                {
                    Key = comment.CommentText,
                    KeyDe = comment.CommentTextDe,
                    KeyRu = comment.CommentTextRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                };

                _translateRepository.Add(translate);
                comment.CommentTextTranslateKey = translate.TranslateKey;
            }

            _commentRepository.Update(comment);
            return new SuccessResult(Messages.EntityUpdated);
        }

        public IResult Delete(Guid id)
        {
            var entity = _commentRepository.GetByIdWithPassive(id);
            if (entity == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }
            var modelActive = !entity.IsActive;
            entity.IsActive = modelActive;
            _commentRepository.Update(entity);
            return new SuccessResult(Messages.EntityUpdated);
        }

        public IDataResult<CommentVm> GetByIdForWebSite(Guid id)
        {
            var entity = _commentRepository.GetAllForWithoutLogin()
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return new ErrorDataResult<CommentVm>(Messages.EntityNotFound);
            }
            var vm = _mapper.Map<CommentVm>(entity);
            return new SuccessDataResult<CommentVm>(vm);
        }

        public IDataResult<IQueryable<CommentVm>> GetListForWebSite()
        {
            var entityList = _commentRepository.GetAllForWithoutLogin().OrderByDescending(x => x.CommentDate);
            var vmList = _mapper.ProjectTo<CommentVm>(entityList);
            return new SuccessDataResult<IQueryable<CommentVm>>(vmList);
        }
    }
}

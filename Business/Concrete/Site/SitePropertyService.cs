using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Concrete
{
    public class SitePropertyService : ISitePropertyService
    {
        private readonly ISitePropertyRepository _sitePropertyRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ITranslateService _translateService;
        private readonly ITranslateRepository _translateRepository;

        public SitePropertyService(ISitePropertyRepository sitePropertyRepository,
                                   IProjectRepository projectRepository,
                                   IMapper mapper,
                                   ITranslateService translateService,
                                   ITranslateRepository translateRepository)
        {
            _sitePropertyRepository = sitePropertyRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
            _translateService = translateService;
            _translateRepository = translateRepository;
        }

        public IDataResult<SitePropertyVm> GetSiteProperty()
        {
            var entity = _sitePropertyRepository.GetAllForWithoutLogin().FirstOrDefault();
            if (entity == null)
            {
                return new ErrorDataResult<SitePropertyVm>(Messages.EntityNotFound);
            }
            var vm = _mapper.Map<SitePropertyVm>(entity);
            return new SuccessDataResult<SitePropertyVm>(vm);
        }
        public IDataResult<List<ProjectTotalVm>> GetProjectCount(List<int> idList)
        {
            var entities = _projectRepository.GetAllForOdata()
                                      .Where(x => idList.Contains(x.CityId))
                                      .GroupBy(x => new { x.CityId, x.City.Name })
                                      .Select(g => new ProjectTotalVm()
                                      {
                                          Id = g.Key.CityId,
                                          Name = g.Key.Name,
                                          Total = g.Count()
                                      }).ToList();
            return new SuccessDataResult<List<ProjectTotalVm>>(entities);
        }

        [ValidationAspect(typeof(SitePropertyUpdateValidation))]
        public IResult Update(SitePropertyUpdateDto dto)
        {
            var entity = _sitePropertyRepository.GetAllForOdata().FirstOrDefault();
            if (entity == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }
            entity = _mapper.Map(dto, entity);

            #region Translate
            var addTranslateList = new List<Translate>();

            var translateAddress = _translateRepository.GetAllForOdata().FirstOrDefault(x => x.Key == entity.Address
                                                                                            && x.KeyDe == entity.AddressDe
                                                                                            && x.KeyRu == entity.AddressRu);
            if (translateAddress == null)
            {
                var TranslateEntity = new Translate()
                {
                    Key = entity.Address,
                    KeyDe = entity.AddressDe,
                    KeyRu = entity.AddressRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                };
                addTranslateList.Add(TranslateEntity);
                entity.AddressTranslateKey = TranslateEntity.TranslateKey;
            }

            var translateAboutUsText = _translateRepository.GetAllForOdata().FirstOrDefault(x => x.Key == entity.AboutUsText
                                                                                          && x.KeyDe == entity.AboutUsTextDe
                                                                                          && x.KeyRu == entity.AboutUsTextRu);
            if (translateAboutUsText == null)
            {
                var TranslateEntity = new Translate()
                {
                    Key = entity.AboutUsText,
                    KeyDe = entity.AboutUsTextDe,
                    KeyRu = entity.AboutUsTextRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                };
                addTranslateList.Add(TranslateEntity);
                entity.AboutUsTranslateKey = TranslateEntity.TranslateKey;
            }

            if (addTranslateList.Any())
            {
                _translateRepository.AddRange(addTranslateList);
            }

            #endregion

            _sitePropertyRepository.Update(entity);
            return new SuccessResult(Messages.EntityUpdated);
        }
    }
}

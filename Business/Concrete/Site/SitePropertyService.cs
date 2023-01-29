using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Concrete
{
    public class SitePropertyService : ISitePropertyService
    {
        private readonly ISitePropertyRepository _sitePropertyRepository;
        private readonly IMapper _mapper;

        public SitePropertyService(ISitePropertyRepository sitePropertyRepository,
                                   IMapper mapper)
        {
            _sitePropertyRepository = sitePropertyRepository;
            _mapper = mapper;
        }

        public IDataResult<SitePropertyVm> GetSiteProperty()
        {
            var entity = _sitePropertyRepository.GetAllForOdata().FirstOrDefault();
            if (entity == null)
            {
                return new ErrorDataResult<SitePropertyVm>(Messages.EntityNotFound);
            }
            var vm = _mapper.Map<SitePropertyVm>(entity);
            return new SuccessDataResult<SitePropertyVm>(vm);
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
            _sitePropertyRepository.Update(entity);
            return new SuccessResult(Messages.EntityUpdated);
        }
    }
}

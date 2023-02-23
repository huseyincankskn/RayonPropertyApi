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
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public SitePropertyService(ISitePropertyRepository sitePropertyRepository, IProjectRepository projectRepository,
                                   IMapper mapper)
        {
            _sitePropertyRepository = sitePropertyRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
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
            _sitePropertyRepository.Update(entity);
            return new SuccessResult(Messages.EntityUpdated);
        }
    }
}

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Repositories;
using Entities.VMs;

namespace Business.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        private readonly ITownRepository _townRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IStreetRepository _streetRepository;
        private readonly IProjectRepository _projectRepository;

        public AddressService(IMapper mapper, ICityRepository cityRepository, ITownRepository townRepository, IDistrictRepository districtRepository, IStreetRepository streetRepository, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _townRepository = townRepository;
            _districtRepository = districtRepository;
            _streetRepository = streetRepository;
            _projectRepository = projectRepository;
        }
        public IDataResult<IQueryable<CityVm>> GetCityList()
        {
            var projectCities = _projectRepository.GetAllForOdata().Select(x => x.CityId).Distinct();
            var entityList = _cityRepository.GetAllForOdata().Where(x => projectCities.Contains(x.Id));
            var vmList = _mapper.ProjectTo<CityVm>(entityList);
            return new SuccessDataResult<IQueryable<CityVm>>(vmList);
        }
        public IDataResult<IQueryable<TownVm>> GetTownList(int cityId)
        {
            var entityList = _townRepository.GetAllForOdata().Where(x => x.CityId == cityId);
            var vmList = _mapper.ProjectTo<TownVm>(entityList);
            return new SuccessDataResult<IQueryable<TownVm>>(vmList);
        }
        public IDataResult<IQueryable<TownVm>> GetTownListFromName(string cityName)
        {
            var cityId = _cityRepository.GetAllForOdata().FirstOrDefault(x => x.Name == cityName)?.Id;
            var entityList = _townRepository.GetAllForOdata().Where(x => x.CityId == cityId);
            var vmList = _mapper.ProjectTo<TownVm>(entityList);
            return new SuccessDataResult<IQueryable<TownVm>>(vmList);
        }
        public IDataResult<IQueryable<DistrictVm>> GetDistrictList(int townId)
        {
            var entityList = _districtRepository.GetAllForOdata().Where(x => x.TownId == townId);
            var vmList = _mapper.ProjectTo<DistrictVm>(entityList);
            return new SuccessDataResult<IQueryable<DistrictVm>>(vmList);
        }
        public IDataResult<IQueryable<DistrictVm>> GetDistrictListFromName(string townName,string cityName)
        {
            var townId = _townRepository.GetAllForOdata().FirstOrDefault(x => x.Name == townName && x.City.Name == cityName)?.Id;
            var entityList = _districtRepository.GetAllForOdata().Where(x => x.TownId == townId);
            var vmList = _mapper.ProjectTo<DistrictVm>(entityList);
            return new SuccessDataResult<IQueryable<DistrictVm>>(vmList);
        }
        public IDataResult<IQueryable<StreetVm>> GetStreetList(int districtId)
        {
            var entityList = _streetRepository.GetAllForOdata().Where(x => x.DistrictId == districtId);
            var vmList = _mapper.ProjectTo<StreetVm>(entityList);
            return new SuccessDataResult<IQueryable<StreetVm>>(vmList);
        }
        public IDataResult<IQueryable<StreetVm>> GetStreetListFromName(string districtName,string townName)
        {
            var districtId = _districtRepository.GetAllForOdata().FirstOrDefault(x => x.Name == districtName && x.Town.Name == townName)?.Id;
            var entityList = _streetRepository.GetAllForOdata().Where(x => x.DistrictId == districtId);
            var vmList = _mapper.ProjectTo<StreetVm>(entityList);
            return new SuccessDataResult<IQueryable<StreetVm>>(vmList);
        }
    }
}

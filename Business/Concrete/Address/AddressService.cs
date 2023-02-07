﻿using AutoMapper;
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

        public AddressService(IMapper mapper, ICityRepository cityRepository, ITownRepository townRepository, IDistrictRepository districtRepository, IStreetRepository streetRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _townRepository = townRepository;
            _districtRepository = districtRepository;
            _streetRepository = streetRepository;
        }
        public IDataResult<IQueryable<CityVm>> GetCityList()
        {
            var entityList = _cityRepository.GetAllForOdata();
            var vmList = _mapper.ProjectTo<CityVm>(entityList);
            return new SuccessDataResult<IQueryable<CityVm>>(vmList);
        }
        public IDataResult<IQueryable<TownVm>> GetTownList(int cityId)
        {
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
        public IDataResult<IQueryable<StreetVm>> GetStreetList(int districtId)
        {
            var entityList = _streetRepository.GetAllForOdata().Where(x => x.DistrictId == districtId);
            var vmList = _mapper.ProjectTo<StreetVm>(entityList);
            return new SuccessDataResult<IQueryable<StreetVm>>(vmList);
        }
    }
}
using Core.Utilities.Results;
using Entities.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAddressService
    {
        IDataResult<IQueryable<CityVm>> GetCityList();
        IDataResult<IQueryable<CityVm>> GetCityListForAdmin();
        IDataResult<IQueryable<TownVm>> GetTownList(int cityId);
        IDataResult<IQueryable<TownVm>> GetTownListFromName(string cityName);
        IDataResult<IQueryable<DistrictVm>> GetDistrictList(int townId);
        IDataResult<IQueryable<DistrictVm>> GetDistrictListFromName(string townName, string cityName);
        IDataResult<IQueryable<StreetVm>> GetStreetList(int districtId);
        IDataResult<IQueryable<StreetVm>> GetStreetListFromName(string districtName, string townName);
    }
}

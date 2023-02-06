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
        IDataResult<IQueryable<TownVm>> GetTownList(int cityId);
        IDataResult<IQueryable<DistrictVm>> GetDistrictList(int townId);
        IDataResult<IQueryable<StreetVm>> GetStreetList(int districtId);
    }
}

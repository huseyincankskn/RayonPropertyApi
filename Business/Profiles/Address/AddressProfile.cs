using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CityVm, City>().ReverseMap();
            CreateMap<TownVm, Town>().ReverseMap();
            CreateMap<DistrictVm, District>().ReverseMap();
            CreateMap<StreetVm, Street>().ReverseMap();
        }
    }
}

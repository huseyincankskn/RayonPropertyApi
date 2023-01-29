using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Profiles.Site
{
    public class SitePropertyProfile : Profile
    {
        public SitePropertyProfile()
        {
            CreateMap<SiteProperty, SitePropertyVm>();
            CreateMap<SitePropertyUpdateDto,SiteProperty>();
        }
    }
}

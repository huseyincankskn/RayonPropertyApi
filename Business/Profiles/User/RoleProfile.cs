using AutoMapper;
using Entities.Concrete;
using Entities.VMs;

namespace Business.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleVm>();
        }
    }
}

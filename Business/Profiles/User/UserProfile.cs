using AutoMapper;
using Entities.Concrete;
using Entities.VMs;

namespace Business.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserVm>()
                .ForMember(dest => dest.Name,
                act => act.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }
}

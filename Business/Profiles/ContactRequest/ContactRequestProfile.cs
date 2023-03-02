using AutoMapper;
using Entities.Concrete;
using Entities.VMs;

namespace Business.Profiles
{
    public class ContactRequestProfile : Profile
    {
        public ContactRequestProfile()
        {
            CreateMap<ContactRequest, ContactRequestEntityVm>();
        }
    }
}

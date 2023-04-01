using AutoMapper;
using Entities.Concrete;
using Entities.VMs;

namespace Business.Profiles
{
    public class TranslateProfile : Profile
    {
        public TranslateProfile()
        {
            CreateMap<Translate,TranslateVm>();
        }
    }
}

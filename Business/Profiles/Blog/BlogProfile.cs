using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Profiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogVm>();
            CreateMap<BlogAddDto, Blog>();
            CreateMap<BlogUpdateDto, Blog>();
        }
    }
}

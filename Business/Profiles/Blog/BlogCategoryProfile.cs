using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Profiles
{
    public class BlogCategoryProfile : Profile
    {
        public BlogCategoryProfile()
        {
            CreateMap<BlogCategory, BlogCategoryVm>();
            CreateMap<BlogCategoryAddDto, BlogCategory>();
            CreateMap<BlogCategoryUpdateDto, BlogCategory>();
        }
    }
}

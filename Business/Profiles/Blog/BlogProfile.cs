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
            CreateMap<Blog, BlogVm>()
                .ForMember(dest => dest.BlogCategoryName,
                act => act.MapFrom(src => src.BlogCategory != null ? src.BlogCategory.Name : null))
                .ForMember(dest => dest.BlogFileName,
                act => act.MapFrom(src => src.BlogFile != null ? src.BlogFile.FileName : string.Empty));
            CreateMap<BlogAddDto, Blog>();
            CreateMap<BlogUpdateDto, Blog>();
        }
    }
}

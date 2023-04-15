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
                act => act.MapFrom(src => src.BlogFile != null ? src.BlogFile.FileName : string.Empty))
                .ForMember(dest => dest.IdString,
                act => act.MapFrom(src => src.Id.ToString("N")))
                .ForMember(dest => dest.BlogCategoryTranslateKey,
                act => act.MapFrom(src => src.BlogCategory.NameTranslateKey));
            CreateMap<BlogAddDto, Blog>();
            CreateMap<BlogUpdateDto, Blog>();
        }
    }
}

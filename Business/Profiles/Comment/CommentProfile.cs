using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentVm>();
            CreateMap<CommentAddDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>();
        }
    }
}

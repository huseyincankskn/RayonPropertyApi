using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class BlogFileProfile : Profile
    {
        public  BlogFileProfile()
        {
            CreateMap<BlogFileAddDto, BlogFile>();
        }
    }
}

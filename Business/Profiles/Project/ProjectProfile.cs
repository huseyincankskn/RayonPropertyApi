using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDto, Project>().ReverseMap();
            CreateMap<ProjectVm, Project>().ReverseMap();
            CreateMap<ProjectFeature, ProjectFeaturesVm>().ReverseMap();
            CreateMap<FeatureVm, Feature>().ReverseMap();
        }
    }
}

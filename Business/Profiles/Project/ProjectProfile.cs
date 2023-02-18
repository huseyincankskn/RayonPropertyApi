using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Enums;
using Entities.VMs;
using Helper.AppSetting;
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
            CreateMap<ProjectVm, Project>().ReverseMap()
                      .ForMember(dest => dest.StreetName,
                        act => act.MapFrom(src => src.Street.Name))
                      .ForMember(dest => dest.TownName,
                        act => act.MapFrom(src => src.Town.Name))
                      .ForMember(dest => dest.DistrictName,
                        act => act.MapFrom(src => src.District.Name))
                      .ForMember(dest => dest.CityName,
                        act => act.MapFrom(src => src.City.Name))
                       .ForMember(dest => dest.StatusValue,
                        act => act.MapFrom(src => (ProjectStatus)src.ProjectStatus))
                       .ForMember(dest => dest.ProjectTypeValue,
                        act => act.MapFrom(src => (ProjectType)src.ProjectTye))
                      .ForMember(dest => dest.PhotoUrls,
                      opt => opt.MapFrom(src => src.ProjectFiles.Where(pf => pf.ProjectId == src.Id).Select(y => AppSettings.ImgUrl + y.FileName)));
            CreateMap<ProjectFeature, ProjectFeaturesVm>().ReverseMap();
            CreateMap<FeatureVm, Feature>().ReverseMap();
        }
    }
}

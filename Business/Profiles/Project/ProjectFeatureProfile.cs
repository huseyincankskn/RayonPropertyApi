﻿using AutoMapper;
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
    public class ProjectFeatureProfile : Profile
    {
        public ProjectFeatureProfile()
        {
            CreateMap<ProjectFeatureDto, ProjectFeature>().ReverseMap();
            CreateMap<ProjectFeaturesVm, ProjectFeature>().ReverseMap();
        }
    }
}

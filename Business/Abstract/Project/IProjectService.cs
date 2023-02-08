﻿using Core.Utilities.Results;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract.Project
{
    public interface IProjectService
    {
        IDataResult<IQueryable<ProjectVm>> GetListQueryableOdata();
        IDataResult<IQueryable<ProjectFeaturesVm>> GetProjectFeatureList();
        IDataResult<ProjectVm> GetById(Guid id);
        IDataResult<ProjectDto> AddProject(ProjectDto project);
        Core.Utilities.Results.IResult Update(ProjectDto dto);
        IDataResult<bool> SaveImages(List<IFormFile> images);
    }
}

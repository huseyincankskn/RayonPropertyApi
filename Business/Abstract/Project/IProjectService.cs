using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.Project
{
    public interface IProjectService
    {
        IDataResult<IQueryable<ProjectVm>> GetListQueryableOdata();
        IDataResult<IQueryable<ProjectFeaturesVm>> GetProjectFeatureList();
        IDataResult<ProjectVm> GetById(Guid id);
        IDataResult<ProjectDto> AddProject(ProjectDto project);
        IDataResult<bool> SaveImages(List<IFormFile> images);
    }
}

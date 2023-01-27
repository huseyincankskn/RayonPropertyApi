using Core.Utilities.Results;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<ProjectDto> AddProject(ProjectDto project);
        IDataResult<bool> SaveImages(List<IFormFile> images);
    }
}

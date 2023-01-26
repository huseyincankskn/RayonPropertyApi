using Business.Abstract;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        public IDataResult<ProjectDto> AddProject(ProjectDto project)
        {
            return new SuccessDataResult<ProjectDto>(project);
        }
    }
}

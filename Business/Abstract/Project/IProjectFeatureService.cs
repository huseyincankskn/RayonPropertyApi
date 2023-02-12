using Core.Utilities.Results;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Abstract
{
    public interface IProjectFeatureService
    {
        IDataResult<IQueryable<ProjectFeaturesVm>> GetListQueryableOdata();
        IDataResult<ProjectFeaturesVm> GetById(short id);
        IDataResult<ProjectFeatureDto> AddProject(ProjectFeatureDto project);
        Core.Utilities.Results.IResult Update(ProjectFeatureDto dto);
        IResult Delete(short id);
    }
}

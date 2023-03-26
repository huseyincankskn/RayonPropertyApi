using Core.Utilities.Results;
using Entities.VMs;

namespace Business.Abstract
{
    public interface IRoleService
    {
        IDataResult<RoleVm> GetById(short id);

        IDataResult<IQueryable<RoleVm>> GetListQueryable();

        RoleVm GetRoleByPath(string path, string method);

        IResult CheckUserRole(Guid userId, short roleId);
    }
}

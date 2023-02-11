using Core.Utilities.Results;
using Entities.VMs;

namespace Business.Abstract
{
    public interface IDashboardService
    {
        IDataResult<DashboardVm> GetDashboardInfos();
    }
}

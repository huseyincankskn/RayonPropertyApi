using Core.Utilities.Results;
using Entities.Dtos;
using Entities.VMs;

namespace Business.Abstract
{
    public interface ISitePropertyService
    {
        IDataResult<SitePropertyVm> GetSiteProperty();

        IResult Update(SitePropertyUpdateDto dto);
    }
}

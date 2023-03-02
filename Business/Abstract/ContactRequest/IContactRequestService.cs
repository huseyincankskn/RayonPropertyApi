using Core.Utilities.Results;
using Entities.VMs;

namespace Business.Abstract.ContactRequest
{
    public interface IContactRequestService
    {
        IDataResult<IQueryable<ContactRequestEntityVm>> GetListQueryableOdata();
        IResult Delete(Guid id);
    }
}

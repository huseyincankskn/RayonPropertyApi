using Core.DataAccess.EntityFramework.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract.EntityFramework.Repository
{
    public interface IContactRequestRepository : IGenericRepository<ContactRequest>
    {
    }
}

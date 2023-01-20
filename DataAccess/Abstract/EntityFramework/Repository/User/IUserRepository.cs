using Core.DataAccess.EntityFramework.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}

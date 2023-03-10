using Core.DataAccess.EntityFramework.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract.EntityFramework.Repository
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
    }
}

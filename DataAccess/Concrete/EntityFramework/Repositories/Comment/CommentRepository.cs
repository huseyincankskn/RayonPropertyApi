using Core.DataAccess.EntityFramework;
using Core.Helpers;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(RayonPropertyContext context, IHttpAccessorHelper httpAccessorHelper) : base(context, httpAccessorHelper)
        {
        }
    }
}

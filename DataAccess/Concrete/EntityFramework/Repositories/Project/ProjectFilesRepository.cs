using Core.DataAccess.EntityFramework;
using Core.Helpers;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class ProjectFilesRepository : GenericRepository<ProjectFiles>, IProjectFilesRepository
    {
        public ProjectFilesRepository(RayonPropertyContext context, IHttpAccessorHelper httpAccessorHelper) : base(context, httpAccessorHelper)
        {
        }
    }
}

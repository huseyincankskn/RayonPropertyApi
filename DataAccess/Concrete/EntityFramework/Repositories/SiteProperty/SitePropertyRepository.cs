using Core.DataAccess.EntityFramework;
using Core.Helpers;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class SitePropertyRepository : GenericRepository<SiteProperty>, ISitePropertyRepository
    {
        public SitePropertyRepository(RayonPropertyContext context, IHttpAccessorHelper httpAccessorHelper) : base(context, httpAccessorHelper)
        {
        }
    }
}

using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class TownRepository : GenericConstantRepository<Town>, ITownRepository
    {
        public TownRepository(RayonPropertyContext context) : base(context)
        {
        }
    }
}

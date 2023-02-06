using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class StreetRepository : GenericConstantRepository<Street>, IStreetRepository
    {
        public StreetRepository(RayonPropertyContext context) : base(context)
        {
        }
    }
}

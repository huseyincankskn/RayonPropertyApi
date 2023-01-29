using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class CityRepository : GenericConstantRepository<City>, ICityRepository
    {
        public CityRepository(RayonPropertyContext context) : base(context)
        {
        }
    }
}

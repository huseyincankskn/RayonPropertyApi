using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class DistrictRepository : GenericConstantRepository<District>, IDistrictRepository
    {
        public DistrictRepository(RayonPropertyContext context) : base(context)
        {
        }
    }
}

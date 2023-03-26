using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class RoleRepository : GenericConstantRepository<Role>, IRoleRepository
    {
        public RoleRepository(RayonPropertyContext context) : base(context)
        {
        }
    }
}

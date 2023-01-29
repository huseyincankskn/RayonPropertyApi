using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class CurrencyRepository : GenericConstantRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(RayonPropertyContext context) : base(context)
        {
        }
    }
}

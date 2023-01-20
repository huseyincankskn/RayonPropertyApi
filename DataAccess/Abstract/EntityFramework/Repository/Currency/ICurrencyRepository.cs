using Core.DataAccess.EntityFramework.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    internal interface ICurrencyRepository : IGenericConstantRepository<Currency>
    {
    }
}

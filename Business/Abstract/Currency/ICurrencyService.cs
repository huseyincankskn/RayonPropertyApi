using Core.Utilities.Results;
using Entities.VMs;

namespace Business.Abstract
{
    public interface ICurrencyService
    {
        IDataResult<CurrencyRateVm> GetCurrencyRates();
    }
}

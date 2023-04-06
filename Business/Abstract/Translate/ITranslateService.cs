using Core.Utilities.Results;
using Entities.VMs;

namespace Business.Abstract
{
    public interface ITranslateService
    {
        IDataResult<IQueryable<TranslateVm>> GetList();

        string GenerateTranslateKey();

        string GenerateUniqueTranslateKey();
    }
}

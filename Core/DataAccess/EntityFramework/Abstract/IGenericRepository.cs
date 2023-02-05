using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Core.DataAccess.EntityFramework.Abstract
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll();
      
        T GetById(Guid id);

        IDataResult<T> Add(T entity);
        IDataResult<T> Update(T entity);

        IDataResult<T> Delete(T entity);

        IQueryable<T> GetAllForOdata();

        IDataResult<List<T>> AddRange(List<T> entities);

        IDataResult<List<T>> UpdateRange(List<T> entities);

        IDataResult<List<T>> DeleteRange(List<T> entities);

        IDataResult<T> HardDelete(T entity);

        IDataResult<T> AddWithoutLogin(T entity);

        IDataResult<T> DeleteWithoutLogin(T entity);

        IDataResult<T> UpdateWithoutLogin(T entity);

        T GetByIdWithPassive(Guid id);

        IQueryable<T> GetAllForOdataWithPassive();
    }
}
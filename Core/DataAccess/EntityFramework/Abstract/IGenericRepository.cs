using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
    }
}
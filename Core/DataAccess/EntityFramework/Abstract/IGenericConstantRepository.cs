using Core.Entities;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework.Abstract
{
    public interface IGenericConstantRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> GetAll();

        IList<T> GetList(Expression<Func<T, bool>> filter = null);

        T Get(Expression<Func<T, bool>> filter);

        T GetById(object id);

        IDataResult<T> Add(T entity);

        public IQueryable<T> GetAllForOdata();
        IDataResult<T> Update(T entity);
        IDataResult<T> HardDelete(T entity);
    }
}
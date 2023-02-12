using Core.DataAccess.EntityFramework.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public class GenericConstantRepository<TEntity> : IGenericConstantRepository<TEntity>
            where TEntity : class, IEntity, new()
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericConstantRepository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public IQueryable<TEntity> GetAllForOdata()
        {
            return DbSet.AsQueryable().AsNoTracking();
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? DbSet.ToList()
                : DbSet.Where(filter).ToList();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.FirstOrDefault(filter);
        }

        public TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public IDataResult<TEntity> Add(TEntity entity)
        {
            var addedEntity = Context.Entry(entity);
            addedEntity.State = EntityState.Added;
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(addedEntity.Entity);
        }

        public IDataResult<TEntity> Update(TEntity entity)
        {
            var updatedEntity = Context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(updatedEntity.Entity);
        }
        public IDataResult<TEntity> HardDelete(TEntity entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(entity);
        }
    }
}
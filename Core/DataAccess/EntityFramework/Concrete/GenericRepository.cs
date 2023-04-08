using Core.DataAccess.EntityFramework.Abstract;
using Core.Entities.Concrete;
using Core.Helpers;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;
        private readonly IHttpAccessorHelper _httpAccessorHelper;

        public GenericRepository(DbContext context, IHttpAccessorHelper httpAccessorHelper)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
            _httpAccessorHelper = httpAccessorHelper;
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable().Where(x => !x.IsDeleted && x.IsActive);
        }
        public virtual IQueryable<TEntity> GetAllForOdata()
        {
            return DbSet.AsQueryable().Where(x => !x.IsDeleted && x.IsActive).AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetAllForOdataWithPassive()
        {
            return DbSet.AsQueryable().Where(x => !x.IsDeleted).AsNoTracking();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(x => !x.IsDeleted && x.IsActive).FirstOrDefault(filter);
        }

        public TEntity GetById(Guid id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id && !x.IsDeleted && x.IsActive);
        }

        public IDataResult<TEntity> Add(TEntity entity)
        {
            entity.AddDate = DateTime.Now;
            entity.AddUserId = _httpAccessorHelper.GetUserId();

            var addedEntity = Context.Entry(entity);
            addedEntity.State = EntityState.Added;
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(addedEntity.Entity);
        }


        public IDataResult<TEntity> Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUserId = _httpAccessorHelper.GetUserId();

            var updatedEntity = Context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(updatedEntity.Entity);
        }

        public IDataResult<TEntity> Delete(TEntity entity)
        {
            entity.DeleteDate = DateTime.Now;
            entity.DeleteUserId = _httpAccessorHelper.GetUserId();
            entity.IsDeleted = true;
            var deletedEntity = Context.Entry(entity);
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(deletedEntity.Entity);
        }

        public IDataResult<TEntity> HardDelete(TEntity entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(entity);
        }

        public IDataResult<List<TEntity>> AddRange(List<TEntity> entities)
        {
            entities.ForEach(x =>
            {
                x.AddDate = DateTime.Now;
                x.AddUserId = _httpAccessorHelper.GetUserId();
            });

            DbSet.AddRange(entities);
            Context.SaveChanges();
            return new SuccessDataResult<List<TEntity>>(entities);
        }
        public IDataResult<List<TEntity>> UpdateRange(List<TEntity> entities)
        {
            entities.ForEach(x =>
            {
                x.UpdateDate = DateTime.Now;
                x.UpdateUserId = _httpAccessorHelper.GetUserId();
            });

            DbSet.UpdateRange(entities);
            Context.SaveChanges();
            return new SuccessDataResult<List<TEntity>>(entities);
        }

        public IDataResult<List<TEntity>> DeleteRange(List<TEntity> entities)
        {
            entities.ForEach(x =>
            {
                x.DeleteDate = DateTime.Now;
                x.DeleteUserId = _httpAccessorHelper.GetUserId();
                x.IsDeleted = true;
            });

            DbSet.UpdateRange(entities);
            Context.SaveChanges();
            return new SuccessDataResult<List<TEntity>>(entities);
        }
        public IDataResult<List<TEntity>> HardDeleteRange(List<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            Context.SaveChanges();
            return new SuccessDataResult<List<TEntity>>(entities);
        }

        public bool Exist(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(x => !x.IsDeleted && x.IsActive).Any(filter);
        }

        public IDataResult<TEntity> AddWithoutLogin(TEntity entity)
        {
            entity.AddDate = DateTime.Now;
            entity.AddUserId = Guid.Empty;

            var addedEntity = Context.Entry(entity);
            addedEntity.State = EntityState.Added;
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(addedEntity.Entity);
        }

        public IDataResult<TEntity> DeleteWithoutLogin(TEntity entity)
        {
            entity.DeleteDate = DateTime.Now;
            entity.DeleteUserId = Guid.Empty;
            entity.IsDeleted = true;

            var deletedEntity = Context.Entry(entity);
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(deletedEntity.Entity);
        }

        public IDataResult<TEntity> UpdateWithoutLogin(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;

            var updatedEntity = Context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(updatedEntity.Entity);
        }

        public TEntity GetByIdWithPassive(Guid id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public TEntity GetWithoutLogin(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(x => !x.IsDeleted && x.IsActive).FirstOrDefault(filter);
        }

        public IQueryable<TEntity> GetAllForWithoutLogin()
        {
            return DbSet.AsQueryable().Where(x => !x.IsDeleted && x.IsActive);
        }

        public TEntity GetByIdWithoutLogin(Guid id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id && !x.IsDeleted && x.IsActive);
        }

        public IDataResult<List<TEntity>> UpdateRangeWithoutLogin(List<TEntity> entities)
        {
            entities.ForEach(x =>
            {
                x.UpdateDate = DateTime.Now;
                x.UpdateUserId = Guid.Empty;
            });

            DbSet.UpdateRange(entities);
            Context.SaveChanges();
            return new SuccessDataResult<List<TEntity>>(entities);
        }
    }
}
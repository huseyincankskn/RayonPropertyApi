using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configuration
{
    public abstract class BaseConstantEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity, new()
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            EntityConfigure(builder);
        }

        public abstract void EntityConfigure(EntityTypeBuilder<TEntity> builder);
    }
}
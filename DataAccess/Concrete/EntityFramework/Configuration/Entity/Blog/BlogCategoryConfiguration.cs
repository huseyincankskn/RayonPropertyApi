using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configuration.Entity
{
    public class BlogCategoryConfiguration : BaseEntityConfiguration<BlogCategory>
    {
        public override void EntityConfigure(EntityTypeBuilder<BlogCategory> builder)
        {
            builder.Property(x => x.Name).IsRequired().NVarChar(50);
        }
    }
}

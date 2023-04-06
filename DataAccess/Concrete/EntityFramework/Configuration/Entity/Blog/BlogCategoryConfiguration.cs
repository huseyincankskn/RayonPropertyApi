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
            builder.Property(x => x.NameDe).IsRequired().NVarChar(50);
            builder.Property(x => x.NameRu).IsRequired().NVarChar(50);
            builder.Property(x => x.NameTranslateKey).NVarChar(20);
        }
    }
}

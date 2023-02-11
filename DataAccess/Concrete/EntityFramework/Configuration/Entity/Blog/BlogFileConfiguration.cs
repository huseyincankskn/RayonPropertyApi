using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccess.Concrete.EntityFramework.Configuration.Entity
{
    public class BlogFileConfiguration : BaseEntityConfiguration<BlogFile>
    {
        public override void EntityConfigure(EntityTypeBuilder<BlogFile> builder)
        {
            builder.Property(x => x.FileName).IsRequired().NVarChar(200);
        }
    }
}

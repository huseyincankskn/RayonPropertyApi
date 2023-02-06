using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class BlogConfiguration : BaseEntityConfiguration<Blog>
    {
        public override void EntityConfigure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasOne(x => x.BlogCategory).
             WithMany(x => x.Blogs)
             .HasForeignKey(x => x.BlogCategoryId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Title).IsRequired().NVarChar(200);
            builder.Property(x => x.Post).IsRequired().NVarChar(4000);
        }
    }
}

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

            builder.HasOne(x => x.BlogFile).
                WithOne(ta => ta.Blog)
                .HasForeignKey<Blog>(x => x.BlogFileId);

            builder.Property(x => x.Title).IsRequired().NVarChar(200);
            builder.Property(x => x.TitleDe).IsRequired().NVarChar(200);
            builder.Property(x => x.TitleRu).IsRequired().NVarChar(200);

            builder.Property(x => x.Post).IsRequired().NVarChar(4000);
            builder.Property(x => x.PostDe).IsRequired().NVarChar(4000);
            builder.Property(x => x.PostRu).IsRequired().NVarChar(4000);

            builder.Property(x => x.TitleTranslateKey).NVarChar(20);
            builder.Property(x => x.PostTranslateKey).NVarChar(20);
        }
    }
}

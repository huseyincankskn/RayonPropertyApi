

using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configuration.Entity
{
    public class CommentConfiguration : BaseEntityConfiguration<Comment>
    {
        public override void EntityConfigure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Name).NVarChar(100);
            builder.Property(x => x.Country).NVarChar(100);
            builder.Property(x => x.CommentText).NVarChar(3000);
        }
    }
}

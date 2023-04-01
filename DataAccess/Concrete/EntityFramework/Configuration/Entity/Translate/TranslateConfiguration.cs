using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configuration.Entity
{
    public class TranslateConfiguration : BaseEntityConfiguration<Translate>
    {
        public override void EntityConfigure(EntityTypeBuilder<Translate> builder)
        {
            builder.Property(x => x.Key).NVarChar(4000);
            builder.Property(x => x.KeyDe).NVarChar(4000);
            builder.Property(x => x.KeyRu).NVarChar(4000);
        }
    }
}

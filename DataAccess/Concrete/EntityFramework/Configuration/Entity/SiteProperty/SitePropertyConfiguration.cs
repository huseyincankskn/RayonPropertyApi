using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class SitePropertyConfiguration : BaseEntityConfiguration<SiteProperty>
    {
        public override void EntityConfigure(EntityTypeBuilder<SiteProperty> builder)
        {
            builder.Property(x => x.Address).NVarChar(1000);
            builder.Property(x => x.Email).NVarChar(100);
            builder.Property(x => x.PhoneNumber).Phone();
            builder.Property(x => x.Name).NVarChar(200);
        }
    }
}

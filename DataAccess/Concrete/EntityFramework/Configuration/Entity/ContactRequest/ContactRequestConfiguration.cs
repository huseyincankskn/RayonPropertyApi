using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configuration.Entity
{
    public class ContactRequestConfiguration : BaseEntityConfiguration<ContactRequest>
    {
        public override void EntityConfigure(EntityTypeBuilder<ContactRequest> builder)
        {
            builder.Property(x => x.Email).IsRequired().NVarChar(100);
            builder.Property(x => x.Name).NVarChar(100);
            builder.Property(x => x.Description).NVarChar(500);
            builder.Property(x => x.PhoneNumber).NVarChar(20);
        }
    }
}

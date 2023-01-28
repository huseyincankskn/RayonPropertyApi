using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void EntityConfigure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email).IsRequired().NVarChar(250);
            builder.Property(x => x.FirstName).IsRequired().NVarChar(250);
            builder.Property(x => x.LastName).IsRequired().NVarChar(250);
            builder.Property(x => x.PhoneNumber).IsRequired().Phone();
        }
    }
}

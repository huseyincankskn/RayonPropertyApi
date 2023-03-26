using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class RoleConfiguration : BaseConstantEntityConfiguration<Role>
    {
        public override void EntityConfigure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.MethodType).NVarChar(50);
            builder.Property(x => x.ControllerName).NVarChar(50);
            builder.Property(x => x.Name).NVarChar(50);
        }
    }
}

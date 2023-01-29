using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class TownConfiguration : BaseConstantEntityConfiguration<Town>
    {
        public override void EntityConfigure(EntityTypeBuilder<Town> builder)
        {
            builder.Property(x => x.Name).NVarChar(100);
        }
    }
}

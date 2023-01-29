using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class CityConfiguration : BaseConstantEntityConfiguration<City>
    {
        public override void EntityConfigure(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.Name).NVarChar(100);
        }
    }
}

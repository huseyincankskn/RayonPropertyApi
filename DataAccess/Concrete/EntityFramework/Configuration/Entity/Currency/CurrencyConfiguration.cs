using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class CurrencyConfiguration : BaseConstantEntityConfiguration<Currency>
    {
        public override void EntityConfigure(EntityTypeBuilder<Currency> builder)
        {
            builder.Property(x => x.Name).NVarChar(50);
        }
    }
}

using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class TownConfiguration : BaseConstantEntityConfiguration<Town>
    {
        public override void EntityConfigure(EntityTypeBuilder<Town> builder)
        {
            builder.Property(x => x.Name).NVarChar(100);
            builder.HasOne(x => x.City).
                WithMany(x => x.Towns)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

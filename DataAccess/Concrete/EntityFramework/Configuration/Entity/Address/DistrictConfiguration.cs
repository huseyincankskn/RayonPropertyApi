using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class DistrictConfiguration : BaseConstantEntityConfiguration<District>
    {
        public override void EntityConfigure(EntityTypeBuilder<District> builder)
        {
            builder.Property(x => x.Name).NVarChar(100);
            builder.HasOne(x => x.Town).
                WithMany(x => x.Districts)
                .HasForeignKey(x => x.TownId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configuration.Entity
{
    public class StreetConfiguration : BaseConstantEntityConfiguration<Street>
    {
        public override void EntityConfigure(EntityTypeBuilder<Street> builder)
        {
            builder.Property(x => x.Name).NVarChar(100);
            builder.HasOne(x => x.District).
                WithMany(x => x.Streets)
                .HasForeignKey(x => x.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

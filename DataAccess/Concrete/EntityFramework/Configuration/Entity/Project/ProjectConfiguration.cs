using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class ProjectConfiguration : BaseEntityConfiguration<Project>
    {
        public override void EntityConfigure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(x => x.ProjectNumber).NVarChar(100);
            builder.HasOne(x => x.City).
                WithMany(x => x.Projects)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Town).
               WithMany(x => x.Projects)
               .HasForeignKey(x => x.TownId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.District).
               WithMany(x => x.Projects)
               .HasForeignKey(x => x.DistrictId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Street).
               WithMany(x => x.Projects)
               .HasForeignKey(x => x.StreetId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

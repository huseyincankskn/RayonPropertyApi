using DataAccess.Concrete.EntityFramework.Configuration;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class UserRoleConfiguration : BaseEntityConfiguration<UserRole>
    {
        public override void EntityConfigure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(sc => new { sc.UserId, sc.RoleId });

            builder
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserRoles)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(pt => pt.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(pt => pt.Role)
                .WithMany(t => t.UserRoles)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(pt => pt.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

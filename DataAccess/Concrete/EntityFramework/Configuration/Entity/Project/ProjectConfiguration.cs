using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class ProjectConfiguration : BaseEntityConfiguration<Project>
    {
        public override void EntityConfigure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(x => x.ProjectNumber).NVarChar(100);
        }
    }
}

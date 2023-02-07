using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class ProjectFeatureConfiguration : BaseConstantEntityConfiguration<ProjectFeature>
    {
        public override void EntityConfigure(EntityTypeBuilder<ProjectFeature> builder)
        {
            builder.Property(x => x.Name).IsRequired().NVarChar(100);
        }
    }
}

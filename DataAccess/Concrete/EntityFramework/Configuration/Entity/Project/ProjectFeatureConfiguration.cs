using DataAccess.Concrete.EntityFramework.Configuration;
using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete
{
    public class ProjectFeatureConfiguration : BaseConstantEntityConfiguration<ProjectFeature>
    {
        public override void EntityConfigure(EntityTypeBuilder<ProjectFeature> builder)
        {
            builder.Property(x => x.Name).IsRequired().NVarChar(100);
            builder.Property(x => x.NameDe).IsRequired().NVarChar(100);
            builder.Property(x => x.NameRu).IsRequired().NVarChar(100);
            builder.Property(x => x.NameTranslateKey).NVarChar(20);
        }
    }
}

using DataAccess.Concrete.EntityFramework.Configuration;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class FeatureConfiguration : BaseEntityConfiguration<Feature>
    {
        public override void EntityConfigure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasOne(pt => pt.ProjectFeature)
                .WithMany(p => p.Features)
                .HasForeignKey(pt => pt.ProjectFeatureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pt => pt.Project)
                .WithMany(t => t.Features)
                .HasForeignKey(pt => pt.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

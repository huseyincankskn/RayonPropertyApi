using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Configuration.Entity
{
    public class ProjectFilesConfiguration : BaseEntityConfiguration<ProjectFiles>
    {
        public override void EntityConfigure(EntityTypeBuilder<ProjectFiles> builder)
        {
            builder.Property(x => x.FileName).NVarChar(200);
            builder.HasOne(x => x.Project).
                WithMany(x => x.ProjectFiles)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        
    }
}

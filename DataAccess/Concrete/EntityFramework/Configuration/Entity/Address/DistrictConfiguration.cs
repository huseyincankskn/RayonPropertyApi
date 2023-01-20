using DataAccess.Concrete.EntityFramework.Configuration;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class DistrictConfiguration : BaseConstantEntityConfiguration<District>
    {
        public override void EntityConfigure(EntityTypeBuilder<District> builder)
        {
            throw new NotImplementedException();
        }
    }
}

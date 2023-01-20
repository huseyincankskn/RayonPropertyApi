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
    public class CityConfiguration : BaseConstantEntityConfiguration<City>
    { 
        public override void EntityConfigure(EntityTypeBuilder<City> builder)
        {
            throw new NotImplementedException();
        }
    }
}

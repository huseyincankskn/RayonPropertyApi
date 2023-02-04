using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Town : BaseConstantEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public short CityId { get; set; }
        public virtual City City { get; set; }
    }
}

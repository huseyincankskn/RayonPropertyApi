using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Street : BaseConstantEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int? Pk { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
    }
}

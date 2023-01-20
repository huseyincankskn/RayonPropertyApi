using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class City : BaseConstantEntity<short>
    {
        public string Name { get; set; } = string.Empty;
    }
}

using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class District : BaseConstantEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int TownId { get; set; }
        public virtual Town Town { get; set; }
        public List<Street> Streets { get; set; }
        public List<Project> Projects { get; set; }
    }
}

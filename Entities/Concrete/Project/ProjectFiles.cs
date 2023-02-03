using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProjectFiles : BaseEntity
    {
        public string FileName { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}

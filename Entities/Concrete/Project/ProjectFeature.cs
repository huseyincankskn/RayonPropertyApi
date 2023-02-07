using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class ProjectFeature : BaseConstantEntity<short>
    {
        public string Name { get; set; }
        public virtual List<Project> Projects { get; set; }
        public virtual List<Feature> Features { get; set; }
    }
}

using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Feature : BaseEntity
    {
        public short ProjectFeatureId { get; set; }
        public virtual ProjectFeature ProjectFeature { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}

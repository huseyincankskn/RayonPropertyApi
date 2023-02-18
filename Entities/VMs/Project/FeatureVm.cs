using Entities.Concrete;

namespace Entities.VMs
{
    public class FeatureVm
    {
        public Guid Id { get; set; }
        public short ProjectFeatureId { get; set; }
        public Guid ProjectId { get; set; }
        public string FeatureName { get; set; }
    }
}

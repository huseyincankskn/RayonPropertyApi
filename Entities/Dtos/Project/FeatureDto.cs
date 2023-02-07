using Entities.Concrete;

namespace Entities.Dto
{
    public class FeatureDto
    {
        public short ProjectFeatureId { get; set; }
        public Guid ProjectId { get; set; }
    }
}

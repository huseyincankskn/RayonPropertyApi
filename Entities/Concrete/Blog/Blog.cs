using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Post { get; set; } = string.Empty;
    }
}

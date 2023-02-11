using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class BlogFile : BaseEntity
    {
        public string FileName { get; set; } = string.Empty;
        public virtual Blog Blog { get; set; } = new Blog();
    }
}

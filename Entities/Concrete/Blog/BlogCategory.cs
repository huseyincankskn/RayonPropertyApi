using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class BlogCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<Blog>? Blogs { get; set; } 
    }
}

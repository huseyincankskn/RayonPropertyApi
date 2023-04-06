using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class BlogCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<Blog>? Blogs { get; set; }
        public string NameDe { get; set; } = string.Empty;
        public string NameRu { get; set; } = string.Empty;
        public string NameTranslateKey { get; set; } = string.Empty;
    }
}

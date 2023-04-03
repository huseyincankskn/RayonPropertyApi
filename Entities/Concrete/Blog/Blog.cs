using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Post { get; set; } = string.Empty;
        public virtual BlogCategory BlogCategory { get; set; } = new BlogCategory();
        public Guid BlogCategoryId { get; set; } = Guid.Empty;
        public virtual BlogFile BlogFile { get; set; } = new BlogFile();
        public Guid BlogFileId { get; set; } = Guid.Empty;

        #region Translate
        public string TitleDe { get; set; } = string.Empty;
        public string TitleRu { get; set;} = string.Empty;
        public string PostDe { get; set; } = string.Empty;
        public string PostRu { get; set;} = string.Empty;
        #endregion
    }
}

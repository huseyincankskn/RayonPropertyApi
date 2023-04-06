using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Comment : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public string CommentText { get; set; } = string.Empty;

        #region Translate
        public string CommentTextDe { get; set; } = string.Empty;
        public string CommentTextRu { get; set; } = string.Empty;
        public string CommentTextTranslateKey { get; set; } = string.Empty;
        #endregion
    }
}

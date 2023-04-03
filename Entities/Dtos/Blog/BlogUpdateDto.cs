namespace Entities.Dtos
{
    public class BlogUpdateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Post { get; set; } = string.Empty;
        public Guid BlogCategoryId { get; set; } = Guid.Empty;
        public string BlogCategoryName { get; set; } = string.Empty;
        public Guid BlogFileId { get; set; } = Guid.Empty;

        #region Translate
        public string TitleDe { get; set; } = string.Empty;
        public string TitleRu { get; set;} = string.Empty;
        public string PostDe { get; set; } = string.Empty;
        public string PostRu { get; set; } = string.Empty;
        #endregion
    }
}

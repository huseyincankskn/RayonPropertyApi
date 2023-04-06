namespace Entities.VMs
{
    public class BlogVm
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Post { get; set; } = string.Empty;
        public DateTime AddDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = false;
        public Guid BlogCategoryId { get; set; } = Guid.Empty;
        public string BlogCategoryName { get; set; } = string.Empty;
        public Guid BlogFileId { get; set; } = Guid.Empty;
        public string BlogFileName { get; set; } = string.Empty;


        #region Translate
        public string TitleDe { get; set; } = string.Empty;
        public string TitleRu { get; set; } = string.Empty;
        public string PostDe { get; set; } = string.Empty;
        public string PostRu { get; set; } = string.Empty;
        public string IdString { get; set; } = string.Empty;
        public string TitleTranslateKey { get; set; } = string.Empty;
        public string PostTranslateKey { get; set; } = string.Empty;
        #endregion
    }
}

namespace Entities.VMs
{
    public class BlogCategoryVm
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public DateTime AddDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public int Count { get; set; }
        public string NameDe { get; set; } = string.Empty;
        public string NameRu { get; set; } = string.Empty;
        public string NameTranslateKey { get; set; } = string.Empty;
    }
}

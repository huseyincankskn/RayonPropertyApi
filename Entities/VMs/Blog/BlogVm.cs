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
    }
}

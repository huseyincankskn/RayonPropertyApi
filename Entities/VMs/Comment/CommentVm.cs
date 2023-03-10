namespace Entities.VMs
{
    public class CommentVm
    {
        public Guid Id { get; set; }
        public DateTime AddDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public string CommentText { get; set; } = string.Empty;
    }
}

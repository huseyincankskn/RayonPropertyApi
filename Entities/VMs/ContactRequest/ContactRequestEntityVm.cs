namespace Entities.VMs
{
    public class ContactRequestEntityVm
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime AddDate { get; set; } = DateTime.Now;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}

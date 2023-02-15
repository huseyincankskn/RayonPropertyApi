namespace Entities.VMs
{
    public class ContactRequestVm
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;

    }
}

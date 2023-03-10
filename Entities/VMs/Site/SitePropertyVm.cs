namespace Entities.VMs
{
    public class SitePropertyVm
    {
        public  Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AboutUsText { get; set; } = string.Empty;
    }
}

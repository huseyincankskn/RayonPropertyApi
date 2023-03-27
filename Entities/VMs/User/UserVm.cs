namespace Entities.VMs
{
    public class UserVm
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime AddDate { get; set; }
        public bool IsFullPageAuth { get; set; } = false;
    }
}

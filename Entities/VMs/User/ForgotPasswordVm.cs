namespace Entities.VMs.User
{
    public class ForgotPasswordVm
    {
        public Guid? PsrGuid { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}

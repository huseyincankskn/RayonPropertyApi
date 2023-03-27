namespace Entities.VMs
{
    public class RoleVm
    {
        public string MethodType { get; set; } = string.Empty;
        public string ControllerName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public short Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public short ModuleId { get; set; }
        public string? ModuleName { get; set; }
    }
}

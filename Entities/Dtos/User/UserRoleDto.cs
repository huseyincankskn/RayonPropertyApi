namespace Entities.Dtos
{
    public class UserRoleDto
    {
        public short RoleId { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
        public bool IsAll { get; set; }
    }
}

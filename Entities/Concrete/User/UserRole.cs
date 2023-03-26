using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = new User();
        public short RoleId { get; set; }
        public virtual Role Role { get; set; } = new Role();
    }
}

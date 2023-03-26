using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Role : BaseConstantEntity<short>
    {
        public string MethodType { get; set; } = string.Empty;
        public string ControllerName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public virtual List<UserRole>? UserRoles { get; set; }
    }
}

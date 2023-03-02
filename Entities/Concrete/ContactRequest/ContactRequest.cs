using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class ContactRequest : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}

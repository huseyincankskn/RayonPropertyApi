using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public byte[] PsrSalt { get; set; }
        public byte[] PsrHash { get; set; }
        public Guid? PsrGuid { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}

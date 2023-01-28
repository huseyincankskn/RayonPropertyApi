using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public byte[] PsrSalt { get; set; } = Array.Empty<byte>();
        public byte[] PsrHash { get; set; } = Array.Empty<byte>();
        public Guid? PsrGuid { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}

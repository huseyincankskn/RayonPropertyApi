namespace Entities.Dtos
{
    public class SitePropertyUpdateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AboutUsText { get; set; } = string.Empty;
    }
}
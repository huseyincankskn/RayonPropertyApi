using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class SiteProperty : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AboutUsText { get; set; } = string.Empty;

        #region Translate
        public string AddressDe { get; set; } = string.Empty;
        public string AddressRu { get; set; } = string.Empty;
        public string AboutUsTextDe { get; set; } = string.Empty;
        public string AboutUsTextRu { get; set; } = string.Empty;
        public string AddressTranslateKey { get; set; } = string.Empty;
        public string AboutUsTranslateKey { get; set; } = string.Empty;
        #endregion
    }
}

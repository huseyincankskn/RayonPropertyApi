using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Translate : BaseEntity
    {
        public string Key { get; set; } = string.Empty;
        public string KeyDe { get; set; } = string.Empty;
        public string KeyRu { get; set; } = string.Empty;
        public bool IsWritten { get; set; } = false;
        public string TranslateKey { get; set; } = string.Empty;
    }
}

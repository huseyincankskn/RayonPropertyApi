namespace Entities.Dtos
{
    public class TranslateAddDto
    {
        public string Key { get; set; } = string.Empty;
        public string KeyDe { get; set; } = string.Empty;
        public string KeyRu { get; set; } = string.Empty;
        public bool IsWritten { get; set; } = false;
    }
}

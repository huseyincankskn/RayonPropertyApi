namespace Entities.Dtos
{
    public class BlogCategoryUpdateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string NameDe { get; set; } = string.Empty;
        public string NameRu { get; set; } = string.Empty;
    }
}

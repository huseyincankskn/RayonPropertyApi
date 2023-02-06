namespace Entities.Dtos
{
    public class BlogCategoryUpdateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
    }
}

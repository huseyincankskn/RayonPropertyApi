namespace Entities.Dtos
{
    public class BlogUpdateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Post { get; set; } = string.Empty;
    }
}

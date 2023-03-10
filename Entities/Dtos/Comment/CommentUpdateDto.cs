namespace Entities.Dtos
{
    public class CommentUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public string CommentText { get; set; } = string.Empty;
    }
}

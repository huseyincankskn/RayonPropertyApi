namespace Entities.Dtos
{
    public class BlogAddDto
    {
        public string Title { get; set; } = string.Empty;
        public string Post { get; set; } = string.Empty;
        public Guid BlogCategoryId { get; set; } = Guid.Empty;
        public string BlogCategoryName { get; set; } = string.Empty;
        public Guid BlogFileId {get; set;} = Guid.Empty;
    }
}

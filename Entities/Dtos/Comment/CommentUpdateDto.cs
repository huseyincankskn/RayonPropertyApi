namespace Entities.Dtos
{
    public class CommentUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public string CommentText { get; set; } = string.Empty;



        #region Translate
        public string CommentTextDe { get; set; } = string.Empty;
        public string CommentTextRu { get; set; } = string.Empty;
        #endregion
    }
}

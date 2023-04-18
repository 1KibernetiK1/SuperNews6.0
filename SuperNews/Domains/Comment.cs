using System.ComponentModel.DataAnnotations;

namespace SuperNews.Domains
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public long NewsId { get; set; }
    }
}

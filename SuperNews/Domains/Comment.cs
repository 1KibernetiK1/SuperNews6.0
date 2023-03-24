using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperNews.Domains
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public long? CommentId { get; set; }

        public int ParentId { get; set; }

        public string CommentText { get; set; }

        public string Username { get; set; }
        public DateTime CommentDate { get; set; }
    }
}

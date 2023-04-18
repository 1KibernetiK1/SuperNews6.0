using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperNews.Domains
{
    [Table("Chat")]
    public class Chat
    {
        [Key]
        public long? ChatId { get; set; }

        public int ParentId { get; set; }

        public string ChatText { get; set; }

        public string Username { get; set; }
        public DateTime ChatDate { get; set; }
    }
}

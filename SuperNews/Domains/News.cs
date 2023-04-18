using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperNews.Domains
{
    public class News
    {
        [Key]
        public long? NewsId { get; set; }

        public string? Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public string ImageUrl { get; set; }


        public virtual Rubric? NewsRubric { get; set; }

        public int? RubricId { get; set; }

        //public Comment Comments { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}

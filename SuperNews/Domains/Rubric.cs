using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperNews.Domains
{
    [Table("Rubrics")]
    public class Rubric
    {
        [Key]
        public long RubricId { get; set; }

        public string? Name { get; set; }

        public virtual List<News> NewsOfRubrics { get; set; }
    }
}

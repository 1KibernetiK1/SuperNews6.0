using SuperNews.Domains;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SuperNews.Models
{
    public class RubricViewModel
    {

        public long? RubricId { get; set; }

        [Display(Name = "Название рубрики")]
        public string? Name { get; set; }

        public RubricViewModel()
        {
            
        }

        public RubricViewModel(Rubric rubric)
        {
            RubricId = rubric.RubricId;
            Name = rubric.Name;
        }

    }
}

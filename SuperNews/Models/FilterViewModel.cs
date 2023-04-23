using Microsoft.AspNetCore.Mvc.Rendering;
using SuperNews.Domains;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SuperNews.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Rubric> rubrics, int? rubric, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            rubrics.Insert(0, new Rubric { Name = "Все", RubricId = 0 });
            Rubrics = new SelectList(rubrics, "RubricId", "Name", rubric);
            SelectedRubric = rubric;
            SelectedName = name;
        }
        public SelectList Rubrics { get; private set; } // список рубрик
        public int? SelectedRubric { get; private set; }   // выбранная рубрика
        public string SelectedName { get; private set; }    // введенное имя
    }
}

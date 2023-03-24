using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Collections.Generic;
using SuperNews.Domains;

namespace SuperNews.Models
{
    public class NewsListViewModel
    {
        public IEnumerable<News> News { get; set; } = new List<News>();
        public SelectList Rubrics { get; set; } = new SelectList(new List<Rubric>(), "RubricId", "Name");
        public string? Name { get; set; }
    }
}

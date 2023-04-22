using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Collections.Generic;
using SuperNews.Domains;

namespace SuperNews.Models
{
    public class NewsListViewModel
    {
        public IEnumerable<News> News { get; set; } 

        public PageViewModel PageViewModel { get; set; }

        public FilterViewModel FilterViewModel { get; set; }


    }
}

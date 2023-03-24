using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using SuperNews.Domains;

namespace SuperNews.Models
{
    public class NewsShortModel
    {
        [HiddenInput(DisplayValue = false)]
        public long? NewsId { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Дата создания")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        public NewsShortModel()
        { }

        public NewsShortModel(News news)
        {
            NewsId = news.NewsId;
            Title = news.Title;
            CreationDate = news.CreationDate;
        }

    }
}

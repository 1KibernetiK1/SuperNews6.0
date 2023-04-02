using Microsoft.AspNetCore.Mvc;
using SuperNews.Domains;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace SuperNews.Models
{
    [AllowAnonymous]
    public class NewsViewModel
    {
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Рубрика")]
        public int Rubric { get; set; }

        public long? NewsId { get; set; }

        [Display(Name = "Заголовок новости")]
        public string Title { get; set; }

        [Display(Name = "Описание новости")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Дата созданиея новости")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Display(Name = "Картинка новости")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        //[Display(Name = "Комментарий")]
        //[DataType(DataType.MultilineText)]
        //public Comment Comments { get; set; }

        public NewsViewModel()
        {

        }
    }
}

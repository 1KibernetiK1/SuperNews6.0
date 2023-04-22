using Microsoft.AspNetCore.Mvc;
using SuperNews.Domains;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperNews.DataAccessLayer;

namespace SuperNews.Models
{
    [AllowAnonymous]
    public class NewsViewModel
    {

        public IFormFile ImageFile { get; set; }

        [Display(Name = "Рубрика")]
        public int? Rubric { get; set; }

        public long? NewsId { get; set; }

        [Required]
        [Display(Name = "Заголовок новости")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Описание новости")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Дата создания новости")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

 
        [Display(Name = "Картинка новости")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        //[Display(Name = "Комментарий")]
        //[DataType(DataType.MultilineText)]
        //public Comment Comments { get; set; }

       
        public int Likes { get; set; }
   
        public int Dislikes { get; set; }

        public News News { get; set; }

        public SelectList Rubrics { get; set; } = new SelectList(new List<Rubric>(), "RubricId", "Name");

        public ApplicationDbContext  _context{ get; set; }

        
        
        public NewsViewModel()
        {
           
        }

        public NewsViewModel(News news)
        {
            Rubric = news.RubricId;
            NewsId = news.NewsId;
            Title = news.Title;
            Description = news.Description;
            CreationDate = news.CreationDate;
            ImageUrl = news.ImageUrl;
        }


    }
}

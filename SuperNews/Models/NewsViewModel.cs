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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SuperNews.Models
{
    [AllowAnonymous]
    public class NewsViewModel
    {
        [Required]
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

        [Required]
        [Display(Name = "Картинка новости")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        //[Display(Name = "Комментарий")]
        //[DataType(DataType.MultilineText)]
        //public Comment Comments { get; set; }

       
        public int Likes { get; set; }
   
        public int Dislikes { get; set; }

        public News News { get; set; }

        public SelectList Rubrics { get; set; }

        public ApplicationDbContext  _context{ get; set; }

        
        
        public NewsViewModel()
        {
           
        }

        public SelectList SelectRubrics { get; private set; } // список рубрик
        public NewsViewModel(List<Rubric> rubrics, int? rubric)
        {
            Rubrics = new SelectList(rubrics, "RubricId", "Name", rubric);
        }


    }
}

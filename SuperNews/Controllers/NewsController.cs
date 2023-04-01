using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SuperNews.Abstract;
using SuperNews.BusinessLogic;
using SuperNews.DataAccessLayer;
using SuperNews.Domains;
using SuperNews.Helpers;
using SuperNews.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SuperNews.Controllers
{
    [AllowAnonymous]
    public class NewsController : Controller
    {
        private readonly IRepository<News> _repositoryNews;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<NewsController> _logger;

        public NewsController(IRepository<News> repositoryArticle, ApplicationDbContext context, UserManager<IdentityUser> userManager, ILogger<NewsController> logger)
        {
            _repositoryNews = repositoryArticle;
            _context = context; 
            _userManager = userManager;
            _logger = logger; 
        }

        public IActionResult BookmarksView(string urlReturn)
        {
            ViewBag.UrlReturn = urlReturn;
            var bookmarks = HttpContext.LoadFromSession<Bookmarks>();
            return View(bookmarks);
        }

        public IActionResult DecreaseNews(long id, string urlReturn)
        {
            var product = _repositoryNews.Read(id);
            if (product == null)
                return View("Error");

            var model = product.Adapt<NewsViewModel>();


            // сохранить продукт в корзину
            var cart = HttpContext.LoadFromSession<Bookmarks>();
            cart.RemoveNews(model);
            HttpContext.SaveToSession(cart);

            return new RedirectResult(urlReturn);
            // return View("CartView", cart);
        }

        public IActionResult IncreaseNews(long id, string urlReturn)
        {
            var product = _repositoryNews.Read(id);
            if (product == null)
                return View("Error");

            var model = product.Adapt<NewsViewModel>();

            // сохранить продукт в корзину
            var cart = HttpContext.LoadFromSession<Bookmarks>();
            cart.AddNews(model);
            HttpContext.SaveToSession(cart);

            return new RedirectResult(urlReturn);
            // return View("CartView", cart);
        }

        public IActionResult AddToBookmarks(long id, string urlReturn)
        {
            var product = _repositoryNews.Read(id);
            if (product == null)
                return View("Error");

            var model = product.Adapt<NewsViewModel>();

            // сохранить продукт в корзину
            var cart = HttpContext.LoadFromSession<Bookmarks>();
            cart.AddNews(model);
            HttpContext.SaveToSession(cart);

            return new RedirectResult(urlReturn);
            // return View("CartView", cart);
        }



        public IActionResult List(int? Rubric, string? name)
        {
            IQueryable<News> news = _context.News.Include(p => p.NewsRubric);

            if (Rubric != null && Rubric != 0)
            {
                news = news.Where(p => p.RubricId == Rubric);
            }
            if (!string.IsNullOrEmpty(name))
            {
                news = news.Where(p => p.Title!.Contains(name));
            }

            List<Rubric> companies = _context.Rubrics.ToList();

            // устанавливаем начальный элемент, который позволит выбрать всех
            companies.Insert(0, new Rubric { Name = "Все", RubricId = 0 });

            NewsListViewModel viewModel = new NewsListViewModel
            {
                News = news.ToList(),
                Rubrics = new SelectList(companies, "RubricId", "Name", Rubric),
                Name = name
            };
            return View(viewModel);

        }

        public IActionResult Details(long id)
        {
            var entity = _repositoryNews.Read(id);
            var model = entity.Adapt<NewsViewModel>();

            return View(model);
        }

        public IActionResult Create(long id)
        {
            var manager = _repositoryNews.Read(id);

            return View(manager);
        }

        [HttpPost]
        public IActionResult Create(NewsViewModel model)
        {
            

          
                UploadImage(model);

                News article = new News()
                {
                    NewsId = model.NewsId,
                    Title = model.Title,
                    RubricId = model.Rubric,
                    CreationDate = model.CreationDate,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl
                };

                _repositoryNews.Create(article);

                return RedirectToAction("List", "News");
            

           
        }

        public IActionResult Delete(long id)
        {
            _repositoryNews.Delete(id);

            return RedirectToAction("List", "News");
        }

        public IActionResult Edit(long id)
        {
            var entity = _repositoryNews.Read(id);
            var model = entity.Adapt<NewsViewModel>();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(NewsViewModel model)
        {
            UploadImage(model);

            News manager = new News()
            {
                NewsId = model.NewsId,
                Title = model.Title,
                RubricId = model.Rubric,
                CreationDate = model.CreationDate,
                Description = model.Description,
                ImageUrl = model.ImageUrl

            };

            if (ModelState.IsValid)
            {
                _repositoryNews.Update(manager);
                return RedirectToAction("List", new { id = model.NewsId });
            }

            return View();
        }

        public IActionResult CommentIndex()
        {
            ViewBag.Comment = _context.Comments.OrderBy(x => x.CommentDate).ToList();
            var model = _context.Comments.OrderBy(x => x.CommentDate).FirstOrDefault();
            return View(model);
        }

        public IActionResult CreateComment(Comment comment)
        {
            var curentuser = _userManager.GetUserName(User);
          
                comment = new Comment()
                {
                    CommentText = comment.CommentText,
                    CommentDate = DateTime.Now,
                    Username = curentuser,
                };
                _context.Add(comment);
                _context.SaveChanges();
                ModelState.Clear();

            
            return RedirectToAction("CommentIndex", "News");

        }

       

        private void UploadImage(NewsViewModel editModel)
        {
            if (editModel.ImageFile == null)
            {
                Debug.WriteLine("картинка не найдена");
                return;
            }

            string ext = Path.GetExtension(editModel.ImageFile.FileName);
            string uniqueName = Guid.NewGuid().ToString() + ext;
            string filename = Path.Combine(
                Directory.GetCurrentDirectory(),
                @"wwwroot\NewsImages",
                uniqueName);

            // сохраняем физический файл на сервер
            using (var stream = System.IO.File.Create(filename))
            {
                editModel.ImageFile.CopyTo(stream);
            }

            // в БД заменить на новое имя файла
            editModel.ImageUrl = uniqueName;
        }

    }
}

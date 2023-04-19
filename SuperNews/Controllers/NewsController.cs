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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SuperNews.Controllers
{
    [AllowAnonymous]
    public class NewsController : Controller
    {
        private readonly IRepository<News> _repositoryNews;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<NewsController> _logger;
        NewsAndComments _newsAndComments =new NewsAndComments();

        public NewsController(IRepository<News> repositoryArticle,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            ILogger<NewsController> logger
            )
        {
            _repositoryNews = repositoryArticle;
            _context = context;
            _userManager = userManager;
            _logger = logger;

            if (!_context.Rubrics.Any())
            {
                Rubric Business = new Rubric { Name = "Бизнес" };
                Rubric Health = new Rubric { Name = "Здоровье" };
                Rubric Crime = new Rubric { Name = "Криминал" };
                Rubric Policy = new Rubric { Name = "Политика" };
                Rubric Economy = new Rubric { Name = "Экономика" };
                Rubric Science = new Rubric { Name = "Наука" };

                _context.Rubrics.AddRange(Business, Health, Crime, Policy, Economy, Science);
                _context.SaveChanges();

            }
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

        [HttpGet]
        public IActionResult Details(long id)
        {
            _context.News.Find(id).Views++;
            _context.SaveChanges();
            _newsAndComments.news = _context.News.Find(id);
            _newsAndComments.comments = _context.Comments.Where(n => n.NewsId == id).ToList();

            return View("Details", _newsAndComments);
        }

        [HttpPost]
        public IActionResult Details(Comment comment)
        {
            if (comment != null)
            {
             
                    Comment cm = new Comment()
                    {
                        CommentText = comment.CommentText,
                        Date = comment.Date,
                        NewsId = comment.NewsId,
                        UserName = comment.UserName,
                    };
                    _context.Comments.Add(cm);
                    _context.SaveChanges();           

            }
            return RedirectToAction("Details", "News", comment.NewsId);
        }

        [Authorize]
        public JsonResult Like(long Id)
        {
            _context.News.Find(Id).Likes++;
            if (_context.News.Find(Id).Dislikes < 0 || _context.News.Find(Id).Dislikes == 0)
            {
                _context.News.Find(Id).Dislikes = 0;
            }
            else
            {
                _context.News.Find(Id).Dislikes--;
            }
            _context.SaveChanges();
            return Json(_context.News.Find(Id).Likes);
        }

        [Authorize]
        public JsonResult Dislike(long Id)
        {
            _context.News.Find(Id).Dislikes++;
            if (_context.News.Find(Id).Likes < 0 || _context.News.Find(Id).Likes == 0)
            {
                _context.News.Find(Id).Likes = 0;
            }
            else
            {
                _context.News.Find(Id).Likes--;
            }
            _context.SaveChanges();
            return Json(_context.News.Find(Id).Dislikes);
        }

        public IActionResult Create(long id, int? Rubric, string? name)
        {
            List<Rubric> companies = _context.Rubrics.ToList();

            NewsViewModel viewModel = new NewsViewModel
            {
                Rubrics = new SelectList(companies, "RubricId", "Name", Rubric),
            };


            return View(viewModel);
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

        public IActionResult ChatIndex()
        {
            ViewBag.Chat = _context.Chat.OrderBy(x => x.ChatDate).ToList();
            var model = _context.Chat.OrderBy(x => x.ChatDate).FirstOrDefault();
            return View(model);
        }

        public IActionResult CreateChat(Chat comment)
        {
            var currentuser = _userManager.GetUserName(User);
          
                comment = new Chat()
                {
                    ChatText = comment.ChatText,
                    ChatDate = DateTime.Now,
                    Username = currentuser,
                };
                _context.Add(comment);
                _context.SaveChanges();
                ModelState.Clear();

            
            return RedirectToAction("ChatIndex", "News");

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

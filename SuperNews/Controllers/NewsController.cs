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
using SuperNews.UsersRoles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SuperNews.Controllers
{
    [Authorize(Roles = AppRoles.Administrator)]
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

            if (!_context.News.Any())
            {
                News news1 = new News
                {
                    Title = "Немецкая компания Henkel продала завод в Челябинской области",
                    Description = "Немецкий концерн Henkel, мировой производитель чистящих и моющих средств, клеевых и прочих материалов, подписал соглашение о продаже своего бизнеса в России консорциуму местных финансовых инвесторов и объявил о прекращении своей деятельности в России. Цена вопроса — 54 миллиарда рублей. Об этом говорится в сообщении компании, опубликованном на ее официальном сайте. Один из 11 заводов концерна работает на Южном Урале — в поселке Роза. — В консорциум, который приобретает бизнес Henkel в России, входят Augment Investments, Kismet Capital Group и Elbrus Services. Все приобретатели имеют установленные и давние деловые отношения в западных странах и не подлежат санкциям ЕС или США, — говорится в официальном сообщении компании. — Согласованная цена покупки составляет 54 миллиарда рублей, что соответствует примерно 600 миллионам евро. Соответствующие российские органы уже одобрили сделку, но окончательное закрытие еще не завершено, добавили представители немецкого концерна.",
                    RubricId = 1,
                    ImageUrl = "31241234123421341234"
                };
                News news2 = new News
                {
                    Title = "Странные фразы и необычное поведение: эти симптомы говорят о приближающейся деменции",
                    Description = "Деменция — это состояние, при котором нарушаются когнитивные функции (память, мышление, понимание, речь, способность ориентироваться в пространстве и другие). Часто перед первыми симптомами люди начинают хуже контролировать свое эмоциональное состояние, падает мотивация и деградирует уровень поведения в обществе. К развитию деменции приводят различные болезни и травмы, которые вызывают повреждение мозга, такие как болезнь Альцгеймера или инсульт. Первые проявления деменции родственники часто списывают на естественные возрастные изменения и воспринимают как должное, но ситуация ухудшается, и когда дело доходит до врача, помочь человеку уже сложно. — Деменции подвержены фактически все — независимо от возраста, положения в обществе, образа жизни. Причем деменция прогрессирует у разных людей совершенно по-разному, — рассказывает медицинская сестра Ольга Котельникова, специалист по обучению АНО ДПО «Мастерская заботы».",
                    RubricId = 2,
                    ImageUrl = "31241234123421341234"
                };
                News news3 = new News
                {
                    Title = "После рейда по челябинским рынкам из России выдворят 17 мигрантов",
                    Description = "Из России выдворят 17 мигрантов после рейда по челябинским рынкам, в результате которого в райотделы доставили порядка 200 человек. Об этом в четверг, 20 апреля, сообщили в пресс-службе регионального ГУ МВД. Масштабную проверку мест пребывания мигрантов провели накануне, 19 апреля. — За прошедший день полицейскими при силовой поддержке ОМОНа управления Росгвардии региона проверен ряд крупных рынков, точек стихийной торговли, строительных объектов, тепличных хозяйств, хостелов, гостиниц и общежитий, — сообщили в областном полицейском главке. — Выявлено и пресечено 118 нарушений миграционного законодательства, из которых 78 — нарушение правил пребывания на территории Российской Федерации.",
                    RubricId = 3,
                    ImageUrl = "31241234123421341234"
                };
                News news4 = new News
                {
                    Title = "В правительстве рассказали о планах на время председательства России в ЕАЭС",
                    Description = "Заместитель председателя правительства Алексей Оверчук во время брифинга рассказал о мероприятиях, запланированных в рамках российского председательства в органах Евразийского экономического союза. В мае это будут мероприятия с участием глав государств, в июне с участием глав правительств государств — членов союза. — Основные мероприятия, которые состоятся в рамках российского председательства, — это заседание Высшего Евразийского экономического совета, будет проведен второй Евразийский экономический форум, также будет проведено заседание Евразийского межправительственного совета, — рассказал Оверчук. — Евразийский экономический форум пройдет 24–25 мая. Его ключевая тема — «Евразийская интеграция в многополярном мире». В работе форума примут участие не менее 2 тысяч человек. Программа форума включает проведение пленарного заседания, а также 35 тематических сессий. Максимально охвачены актуальные сферы взаимодействия в рамках ЕАЭС. Евразийский межправительственный совет пройдет в июне. Тогда же будет проведена выставка «Евразия — наш дом», где будут представлены совместные проекты в различных сферах. В июне же состоится заседание Совета глав правительств СНГ.",
                    RubricId = 4,
                    ImageUrl = "31241234123421341234"
                };
                News news5 = new News
                {
                    Title = "На экономическом форуме в Петербурге пройдет Международный чемпионат по кибербезопасности",
                    Description = "Международный киберчемпионат по информационной безопасности пройдет на Петербургском международном экономическом форуме — 2023. Организаторами выступят Минцифры РФ и «Ростелеком». Соревнования пройдут на базе платформы Национального киберполигона, созданием которой занимается «РТК-Солар». 40 команд из разных стран отработают международное взаимодействие при отражении нескольких волн атак государственного масштаба. Чемпионат направлен на координацию усилий по борьбе с хакерами на мировом уровне и развитие международного сообщества. — В условиях стремительно меняющегося мира важно налаживать взаимодействие между специалистами из разных стран, — подчеркнул заместитель председателя Правительства РФ Дмитрий Чернышенко. — Киберчемпионат — не просто соревнование, но и крупная международная площадка для демонстрации передовых отечественных технологий, а также для обмена опытом по отражению масштабных атак.",
                    RubricId = 5,
                    ImageUrl = "31241234123421341234"
                };
                News news6 = new News
                {
                    Title = "Галактика и развлечения: челябинец сфотографировал зеленую комету. Смотрим космические снимки",
                    Description = "Челябинец Андрей Попов минувшей ночью, 1 февраля, сфотографировал зеленую комету. Чтобы сделать кадры отличного качества, он отправился в Южноуральск — за 120 километров от города. — Погоды не было. По сайтам, интерактивным картам, прогнозам всех синоптиков вычислил, где и во сколько будет прозрачное небо. И то обманулся. Рассчитывал в 2 часа ночи, а в реальности распогодилось только в четвертом часу. Замерз там в поле весь насквозь. Еще и Луна мощная и на северо-востоке, близко, засвечивает. Сама комета, напомню, на севере, — рассказал Андрей Попов.",
                    RubricId = 6,
                    ImageUrl = "31241234123421341234"
                };
                News news7 = new News
                {
                    Title = "Центр семейной медицины показал передовые технологии в программах ЭКО",
                    Description = "Специалисты «Центра семейной медицины», старейшей на Урале клиники репродукции человека, показали передовые технологии в программах экстракорпорального оплодотворения. — В следующем году центр отметит свое 22-летие. Филиал ЦСМ в Челябинске открылся в 2015 году, за время его работы благодаря специалистам центра на свет появилось более тысячи детей, зачатых «в пробирке». В клинику обращаются пациенты из разных территорий: Челябинск и Челябинская область, ЯНАО, ХМАО, Оренбургская область, Курганская область, Башкирия, Камчатка, Америка, Израиль, Эстония и Казахстан, — рассказала главный врач филиала «Центра семейной медицины» в Челябинске Евгения Привалова.",
                    RubricId = 2,
                    ImageUrl = "31241234123421341234"
                };
                News news8 = new News
                {
                    Title = "На челябинца, избившего в автобусе школьницу и двух женщин, завели уголовное дело о хулиганстве",
                    Description = "В отношении мужчины, избившего школьницу и двух женщин в автобусе № 64, возбудили уголовное дело по статье о хулиганстве. Об этом во вторник, 18 апреля, сообщил старший помощник руководителя регионального управления Следственного комитета Владимир Шишков. — Возбуждено уголовное дело в отношении 36-летнего жителя Челябинска, подозреваемого в совершении преступления, предусмотренного пунктами «а», «в» части 1 статьи 213 УК РФ, — рассказал представитель СК. — Сотрудниками МВД подозреваемый установлен и доставлен полицию для проведения следственных действий.",
                    RubricId = 3,
                    ImageUrl = "31241234123421341234"
                };
                News news9 = new News
                {
                    Title = "Владимир Путин подписал закон об электронных повестках и едином реестре военнообязанных",
                    Description = "Сегодня, 14 апреля, Владимир Путин подписал закон об электронных повестках и едином реестре военнообязанных. Согласно закону, электронная повестка будет считаться врученной с момента ее размещения в личном кабинете гражданина на «соответствующем информационном ресурсе, в информационной системе». Уклонистов теперь будут ждать дополнительные меры — например, запрет брать кредиты или управлять транспортом. Мы подробно разбирали, что именно изменится в жизни россиян после подписания закона.",
                    RubricId = 4,
                    ImageUrl = "31241234123421341234"
                };
                News news10 = new News
                {
                    Title = "Суд запретил Wildberries брать деньги за возврат брака. Что дальше?",
                    Description = "Плату за возврат некачественных товаров на Wildberries признали незаконной. Об этом сообщает РБК. В суде маркетплейсу противостояло подмосковное управление Роспотребнадзора. Об исходе дела журналистам сообщил Николай Дрозд, замруководителя управления. Он подчеркнул, что если покупатель нашел у товара недостатки, о которых его не предупредил продавец, то покупатель вправе: требовать замену на продукцию той же марки и модели (или другой, с перерасчетом цены); требовать безвозмездного устранения недостатков; отказаться от покупки и потребовать возврата денег; требовать полного возмещения убытков, причиненных из-за продажи некачественного товара. Таким образом, как считают в ведомстве, Wildberries платным возвратом нарушает закон. Подольский городской суд Московской области согласился с этим доводом. Более того, после вступления решения суда в силу клиенты маркетплейса смогут потребовать возврата денег, которые уже заплатили за доставку. Однако в компании с решением суда не согласны. Wildberries собирается обжаловать его. — Суд в полной мере не разобрался в обстоятельствах дела: на нем не присутствовали представители компании, так как не были извещены надлежащим образом, и не смогли представить свои аргументы, — заявил РБК представитель маркетплейса.",
                    RubricId = 1,
                    ImageUrl = "31241234123421341234"
                };

                _context.News.AddRange(news1, news2, news3, news4, news5, news6, news7, news8, news9, news10);
                _context.SaveChanges();
            }
        }

        [Authorize]
        public IActionResult BookmarksView(string urlReturn)
        {
            ViewBag.UrlReturn = urlReturn;
            var bookmarks = HttpContext.LoadFromSession<Bookmarks>();
            return View(bookmarks);
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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


        [AllowAnonymous]
        public IActionResult List(int? Rubric, string? name, int page = 1)
        {
            int pageSize = 5;

            IQueryable<News> news = _context.News.Include(p => p.NewsRubric);

            if (Rubric != null && Rubric != 0)
            {
                news = news.Where(p => p.RubricId == Rubric);
            }
            if (!string.IsNullOrEmpty(name))
            {
                news = news.Where(p => p.Title!.Contains(name));
            }

            var count =  news.Count();
            var items =  news.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            NewsListViewModel viewModel = new NewsListViewModel
            {
                News = items,
                PageViewModel = new PageViewModel(count, page, pageSize),
                FilterViewModel = new FilterViewModel(_context.Rubrics.ToList(), Rubric, name),
            };
            return View(viewModel);

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(long id)
        {
            if (_context.News.Find(id) != null)
            {
                _context.News.Find(id).Views++;
                _context.SaveChanges();
                _newsAndComments.news = _context.News.Find(id);
                _newsAndComments.comments = _context.Comments.Where(n => n.NewsId == id).ToList();
            }

            return View("Details", _newsAndComments);
        }

        [AllowAnonymous]
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

        [Authorize(Roles = $"{AppRoles.Moderator}, {AppRoles.Administrator}")]
        public IActionResult DeleteComment(int id)
        {
         
                var entry = _context.Comments.Find(id);
                _context.Comments.Remove(entry);
                _context.SaveChanges();

            return RedirectToAction("List", "News");
        }

        [Authorize]
        public JsonResult Like(long Id)
        {
            _context.News.Find(Id).Likes++;
            if (_context.News.Find(Id).Dislikes < 0 || _context.News.Find(Id).Dislikes == 0)
            {
                _context.News.Find(Id).Dislikes = 0;
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
            _context.SaveChanges();
            return Json(_context.News.Find(Id).Dislikes);
        }

        [Authorize(Roles = $"{AppRoles.Redactor}, {AppRoles.Administrator}")]
        public IActionResult Create(long id, int? Rubric, string? name)
        {
            List<Rubric> companies = _context.Rubrics.ToList();

            NewsViewModel viewModel = new NewsViewModel
            {
                Rubrics = new SelectList(companies, "RubricId", "Name", Rubric),
            };


            return View(viewModel);
        }

        [Authorize(Roles = $"{AppRoles.Redactor}, {AppRoles.Administrator}")]
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

        [Authorize(Roles = $"{AppRoles.Moderator}, {AppRoles.Administrator}")]
        public IActionResult Delete(long id)
        {
            _repositoryNews.Delete(id);

            return RedirectToAction("List", "News");
        }

        [Authorize(Roles = $"{AppRoles.Moderator}, {AppRoles.Administrator}, {AppRoles.Redactor}")]
        public IActionResult Edit(long id, int? Rubric, string? name)
        {
            var entity = _repositoryNews.Read(id);
            var model = entity.Adapt<NewsViewModel>();


            return View(model);
        }

        [Authorize(Roles = $"{AppRoles.Moderator}, {AppRoles.Administrator}, {AppRoles.Redactor}")]
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

        [Authorize]
        public IActionResult ChatIndex()
        {
            ViewBag.Chat = _context.Chat.OrderBy(x => x.ChatDate).ToList();
            var model = _context.Chat.OrderBy(x => x.ChatDate).FirstOrDefault();
            return View(model);
        }

        [Authorize]
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


        [Authorize(Roles = $"{AppRoles.Moderator}, {AppRoles.Administrator}, {AppRoles.Redactor}")]
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

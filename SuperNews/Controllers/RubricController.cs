using Microsoft.AspNetCore.Mvc;
using SuperNews.Abstract;
using SuperNews.Domains;
using SuperNews.Models;

namespace SuperNews.Controllers
{
    public class RubricController : Controller
    {
        private readonly IRepository<Rubric> _repositoryRubric;
        public RubricController(IRepository<Rubric> repositoryRubric)
        {
            _repositoryRubric = repositoryRubric;
        }

        public IActionResult Create(long id)
        {
            var manager = _repositoryRubric.Read(id);

            return View(manager);
        }

        [HttpPost]
        public IActionResult Create(RubricViewModel model)
        {

            Rubric article = new Rubric()
            {
                RubricId = model.RubricId,
                Name = model.Name,
            };

            _repositoryRubric.Create(article);

            return RedirectToAction("List", "Rubric");



        }

    }
}

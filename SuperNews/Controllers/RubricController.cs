using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperNews.Abstract;
using SuperNews.Domains;
using SuperNews.Models;
using SuperNews.UsersRoles;
using System.Data;

namespace SuperNews.Controllers
{
    [Authorize(Roles = AppRoles.Administrator)]
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

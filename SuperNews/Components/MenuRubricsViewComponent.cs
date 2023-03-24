using Microsoft.AspNetCore.Mvc;
using SuperNews.Abstract;
using SuperNews.Domains;
using System.Linq;

namespace SuperNews.Components
{
    [ViewComponent]
    public class MenuRubricsViewComponent : ViewComponent
    {
        private readonly IRepository<Rubric> repository;

        public MenuRubricsViewComponent(IRepository<Rubric> repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var categoriesNameList = repository
                .GetList()
                .Select(x => x.Name);

            return View(categoriesNameList);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SuperNews.Abstract;
using SuperNews.Domains;
using System.Collections.Generic;
using System.Linq;

namespace SuperNews.DataAccessLayer
{
    public class NewsSqlRepository : IRepository<News>
    {
        private readonly ApplicationDbContext _context;

        public NewsSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(News model)
        {
            _context.News.Add(model);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entry = _context.News.Find(id);
            _context.News.Remove(entry);
            _context.SaveChanges();
        }

        public News FindByName(string name)
        {
            return _context.News
               .FirstOrDefault(c => c.Title == name);
        }

        public IEnumerable<News> GetList()
        {
            return _context
              .News;

        }

        public News Read(long id)
        {
            var entry = _context
             .News
             .FirstOrDefault(p => p.NewsId == id);
            return entry;
        }

        public News ReadWithRelations(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(News model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

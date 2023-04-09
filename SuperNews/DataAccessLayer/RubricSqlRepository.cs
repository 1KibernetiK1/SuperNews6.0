using Microsoft.EntityFrameworkCore;
using SuperNews.Abstract;
using SuperNews.Domains;

namespace SuperNews.DataAccessLayer
{
    public class RubricSqlRepository : IRepository<Rubric>
    {
        private readonly ApplicationDbContext _context;

        public RubricSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Rubric model)
        {
            _context.Rubrics.Add(model);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entry = _context.Rubrics.Find(id);
            _context.Rubrics.Remove(entry);
            _context.SaveChanges();
        }

        public Rubric FindByName(string name)
        {
            return _context.Rubrics
               .FirstOrDefault(c => c.Name == name);
        }

        public IEnumerable<Rubric> GetList()
        {
            return _context
            .Rubrics;
        }

        public Rubric Read(long id)
        {
            var entry = _context
              .Rubrics
              .FirstOrDefault(p => p.RubricId == id);
            return entry;
        }

        public Rubric ReadWithRelations(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(Rubric model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

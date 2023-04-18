using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperNews.Domains;

namespace SuperNews.DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<News> News { get; set; }

        public DbSet<Rubric> Rubrics { get; set; }

        public DbSet<Chat> Chat { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}

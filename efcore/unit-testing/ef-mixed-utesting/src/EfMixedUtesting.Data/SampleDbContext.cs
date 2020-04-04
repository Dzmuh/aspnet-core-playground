using EfMixedUtesting.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace EfMixedUtesting.Data
{
    public class SampleDbContext: DbContext
    {
        public SampleDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleContent> ArticleContentes { get; set; }
    }
}

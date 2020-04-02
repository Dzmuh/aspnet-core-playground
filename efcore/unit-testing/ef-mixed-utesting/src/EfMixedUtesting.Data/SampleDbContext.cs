using Microsoft.EntityFrameworkCore;

namespace EfMixedUtesting.Data
{
    public class SampleDbContext: DbContext
    {
        public SampleDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}

using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using EfMixedUtesting.Data;

namespace EfMixedUtesting.Tests
{
    public abstract class TestBase
    {
        private bool _useSqlite;

        public async Task<SampleDbContext> GetDbContext()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            if (this._useSqlite)
            {
                // Использую SQLite в качестве базы данных. Проецирую источник данных в память. Удобненько!
                builder.UseSqlite("DataSource=:memory:", x => { });
            }
            else
            {
                // Использую In-Memory DB.
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).ConfigureWarnings(w =>
                {
                    w.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                });
            }

            var dbContext = new SampleDbContext(builder.Options);
            if (this._useSqlite)
            {
                // SQLite нуждается в открытии соединения с базой данных.
                // Это не требуется для InMemoryDatabase и для Microsoft SQL Server.
                await dbContext.Database.OpenConnectionAsync();
            }

            await dbContext.Database.EnsureCreatedAsync();

            return dbContext;
        }

        public void UseSqlite()
        {
            this._useSqlite = true;
        }
    }
}

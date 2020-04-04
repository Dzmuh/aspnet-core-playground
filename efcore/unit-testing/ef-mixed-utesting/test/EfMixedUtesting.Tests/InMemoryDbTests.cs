using EfMixedUtesting.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EfMixedUtesting.Tests
{
    public class InMemoryDbTests : TestBase
    {
        public InMemoryDbTests() { }

        [Fact]
        public async Task ShouldBeAbleToAddAndGetEntity()
        {
            // Подготовка
            using var context = await GetDbContext();
            Guid id = Guid.NewGuid();
            context.Articles.Add(new Article
            {
                Id = id,
                Title = "Article Title",
                Content = new ArticleContent
                {
                    Content = "Article content"
                }
            });
            await context.SaveChangesAsync();

            // Выполняем
            var data = await context.Articles.ToListAsync();

            // Проверяем
            Assert.Single(data);
            Assert.Contains(data, d => d.Id == id);
            Assert.Contains(data, d => d.Title == "Article Title");
            Assert.Contains(data, d => d.Content.Content == "Article content");
        }

        /// <summary>
        /// При использовании In-Memory DB мы не можем выполнять SQL запросы.
        /// Этот тест проваливается и не случайно.
        /// </summary>
        [Fact]
        [Trait("DbDependat", "")]
        public async Task ShouldNotBeAbleToExecuteSql()
        {
            // Подготовка
            using var context = await GetDbContext();

            Guid id = Guid.NewGuid();
            context.Articles.Add(new Article
            {
                Id = id,
                Title = "Article Title",
                Content = new ArticleContent
                {
                    Content = "Article content"
                }
            });

            await context.SaveChangesAsync();

            // Выполняем
            var result = await context.Articles
                .FromSqlRaw("select * from Articles")
                .ToListAsync();

            // Код ниже никогда не будет выполнен.
            // В коде выше мы получим System.NotImplementedException.
            Assert.Equal(id, result.First().Id);
        }
    }
}

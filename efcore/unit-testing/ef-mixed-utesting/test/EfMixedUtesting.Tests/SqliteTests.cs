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
    public class SqliteTests : TestBase
    {
        public SqliteTests()
        {
            UseSqlite();
        }

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
        /// При использовании SQLite мы можем выполнять SQL запросы.
        /// </summary>
        [Fact]
        [Trait("DbDependat", "")]
        public async Task ShouldBeAbleToExeuteSql()
        {
            // Подготовка.
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

            // Выполняем.
            var result = await context.Articles
                .FromSqlRaw("select * from Articles")
                .ToListAsync();

            // Запрос должен получить первую запись и преобразовать byte[] в GUID.
            Assert.Equal(id, result.First().Id);
        }

        /// <summary>
        /// SQL позволяет проверять факт того что ограничения установлены корректно.
        /// И SQLite позволяет проверять большинство простых ограничений.
        /// Например, при вставке сущности которая ссылается на другую связанную сущность,
        /// SQLite проверит что вы ссылаетесь на сущность которая существует.
        /// </summary>
        [Fact]
        [Trait("DbDependat", "")]
        public async Task ShouldFailWhenIncludeIsNotUsed()
        {
            // Подготовка.
            using var context = await GetDbContext();

            // Добавляем сущность которая ссылается на ID несуществующей дочерней сущности.
            context.Articles.Add(new Article
            {
                Title = "Article Title",
                ContentId = Guid.NewGuid()
            });

            // Выполняем и проверяем.
            // И получаем ожидаемое исключение.
            await Assert.ThrowsAsync<DbUpdateException>(
                () => context.SaveChangesAsync());

            // Эта строка пройдёт проверку в тестах с SQLite.
            var articles = await context.Articles.ToListAsync();
            Assert.Empty(articles);
        }

        /// <summary>
        /// SQLite работает медленнее чем In-memory DB.
        /// Очевидно что использование SQLite оправдано при работе с SQL запросами и когда необходимо тестировать ограничения.
        /// </summary>
        /// <remarks>
        /// По поводу скорости. Тесты показывают что при использовании In-memory DB мы получаем ускорение выполнения теста в 2-ва раза.
        /// </remarks>
        [Fact]
        [Trait("Performance", "SQLite")]
        public async Task PerformanceTestSqlite()
        {
            using var context = await GetDbContext();
            for (int i = 0; i < Constants.NumberOfEntitiesToAdd; ++i)
            {
                context.Articles.Add(new Article
                {
                    Title = "Article Title",
                    Content = new ArticleContent
                    {
                        Content = "Article content"
                    }
                });
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// SQLite работает медленнее чем In-memory DB.
        /// AddRange работает быстрее для SQLite, но тоже самое касается и In-memory DB.
        /// </summary>
        [Fact]
        [Trait("Performance", "SQLite")]
        public async Task PerformanceTestBitFaster()
        {
            using var context = await GetDbContext();

            var articles = new List<Article>(Constants.NumberOfEntitiesToAdd);
            for (int i = 0; i < Constants.NumberOfEntitiesToAdd; ++i)
            {
                articles.Add(new Article
                {
                    Title = "Article Title",
                    Content = new ArticleContent
                    {
                        Content = "Article content"
                    }
                });
            }

            context.Articles.AddRange(articles);
            await context.SaveChangesAsync();
        }
    }
}

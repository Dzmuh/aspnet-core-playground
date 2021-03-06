﻿using EfMixedUtesting.Data.Domain;
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
        /// <remarks>
        /// Этот тест проваливается вполне закономерно.
        /// </remarks>
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

        /// <summary>
        /// SQL позволяет проверять факт того что ограничения установлены корректно.
        /// In-Memory DB не проверяет ограничения.
        /// Вы можете спокойно добавлять сущности имеющие ссылки на несуществующие сущности и не получать при этом никаких исключений.
        /// </summary>
        /// <remarks>
        /// Этот тест проваливается вполне закономерно.
        /// </remarks>
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
            // И тут утверждение не сработает. Не будет выброшено никакого исключения.
            await Assert.ThrowsAsync<DbUpdateException>(
                () => context.SaveChangesAsync());

            // This line will never be reached.
            // Эта строка пройдёт проверку в тестах с In-Memory DB.
            var articles = await context.Articles.ToListAsync();
            Assert.Empty(articles);
        }

        /// <summary>
        /// In-memory DB значительно быстрее чем SQLite.
        /// Используйте её в тестах где нет необходимо тестировать ограничения и работать с SQL.
        /// </summary>
        /// <remarks>
        /// По поводу скорости. Тесты показывают что при использовании In-memory DB мы получаем ускорение выполнения теста в 2-ва раза.
        /// </remarks>
        [Fact]
        [Trait("Performance", "InMemory")]
        public async Task PerformanceTestInMemory()
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
        /// In-memory DB работает быстрее чем SQLite.
        /// AddRange работает быстрее для SQLite, но тоже самое касается и In-memory DB.
        /// </summary>
        [Fact]
        [Trait("Performance", "InMemory")]
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

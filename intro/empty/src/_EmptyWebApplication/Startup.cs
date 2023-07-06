using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmptyWebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        /// <summary>
        /// Компоненты middleware конфигурируются с помощью методов расширений Run, Map и Use объекта IApplicationBuilder,
        /// который передается в метод Configure() класса Startup.
        /// Каждый компонент может быть определен как анонимный метод (встроенный inline компонент), либо может быть вынесен в отдельный класс.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Все вызовы типа app.UseXXX представляют собой добавление компонентов middleware для обработки запроса.
            if (env.IsDevelopment())
            {
                // Компонент обработки ошибок - Diagnostics. Добавляется через app.UseDeveloperExceptionPage()
                app.UseDeveloperExceptionPage();
            }

            // Компонент маршрутизации - EndpointRoutingMiddleware. Добавляется через app.UseRouting()
            app.UseRouting();

            // Компонент EndpointMiddleware, который отправляет ответ, если запрос пришел по маршруту "/" (то есть пользователь обратился к корню веб-приложения).
            // Добавляется через метод app.UseEndpoints()
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            // Порядок определения компонентов играет большую роль.
            // Если мы изменим порядок, то приложение нормально работать не будет.
        }
    }
}

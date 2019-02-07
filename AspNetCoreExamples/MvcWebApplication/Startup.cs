﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MvcWebApplication.Data;

namespace MvcWebApplication
{
    /// <summary>
    /// Класс Startup является входной точкой в приложение ASP.NET Core.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// При запуске приложения сначала срабатывает конструктор, затем метод <see cref="ConfigureServices"/> и в конце метод <see cref="Configure"/>.
        /// Эти методы вызываются средой выполнения ASP.NET.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Необязательный метод <see cref="ConfigureServices"/> регистрирует сервисы, которые используются приложением.
        /// В качестве параметра он принимает объект <see cref="IServiceCollection"/>, который и представляет коллекцию сервисов в приложении.
        /// С помощью методов расширений этого объекта производится конфигурация приложения для использования сервисов.
        /// Все методы имеют форму Add[название_сервиса].
        /// </summary>
        /// <param name="services">
        /// Коллекция сервисов в приложении.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Метод services.AddMvc() добавляет в коллекцию сервисов сервисы ApNetCore MVC.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        /// <summary>
        /// Метод <c>Configure</c> устанавливает, как приложение будет обрабатывать запрос. Этот метод является обязательным.
        /// Для установки компонентов, которые обрабатывают запрос, используются методы объекта <see cref="IApplicationBuilder"/>. Объект <c>IApplicationBuilder</c> является обязательным параметром для метода <c>Configure</c>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Кроме того, метод нередко принимает еще два необязательных параметра: <see cref="IHostingEnvironment"/> и <c>ILoggerFactory</c>.
        /// </para>
        /// <para><c>IHostingEnvironment</c>: позволяет взаимодействовать со средой, в которой запускается приложение.</para>
        /// <para><c>ILoggerFactory</c>: предоставляет механизм логгирования в приложении</para>
        /// <para>
        /// В принципе, в метод Configure в качестве параметра может передаваться любой сервис, который зарегистрирован в методе ConfigureServices или который регистрируется для приложения по умолчанию.
        /// </para>
        /// </remarks>
        /// <param name="app">
        /// Объект IApplicationBuilder является обязательным параметром для метода Configure
        /// </param>
        /// <param name="env">
        /// IHostingEnvironment позволяет взаимодействовать со средой, в которой запускается приложение
        /// </param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // С помощью метода UseHsts() объекта IApplicationBuilder мы отправляем браузеру заголовок Strict-Transport-Security, который устанавливает глобально политику работы с определенным доменом и его поддоменами.
                //
                // Подробнее:
                // - https://ru.wikipedia.org/wiki/HSTS
                // - https://docs.microsoft.com/ru-ru/aspnet/core/security/enforcing-ssl
                // 
                // В этом примере метод UseHsts вызывается, если только приложение уже развернуто для полноценного использования, потому что в процессе разработки использование данного метода может создавать неудобства, так как заголовки кэшируются.
                // Кстати, с помощью метода AddHsts в методе ConfigureServices, можно настроить параметры заголовка Strict-Transport-Security. 
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            // Метод устанавливает компоненты MVC для обработки запроса и, в частности, настраивает систему маршрутизации в приложении.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

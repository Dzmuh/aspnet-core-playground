using BlazorServerApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorServerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Приложение в ASP.NET Core представляет объект Microsoft.AspNetCore.Builder.WebApplication.
            // Этот объект настраивает всю конфигурацию приложения, его маршруты, используемые зависимости и т.д..
            //
            // Для создания объекта WebApplication необходим специальный класс-строитель - WebApplicationBuilder.
            // В файле Program.cs вначале создается данный объект с помощью статического метода WebApplication.CreateBuilder:
            var builder = WebApplication.CreateBuilder(args);
            // ⬆ В качестве параметра в метод передаются аргументы, которые передаются приложению при запуске.

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            // Получив объект WebApplicationBuilder, у него вызывается метод Build(), который собствено и создает объект WebApplication:
            var app = builder.Build();
            // ⬆ С помощью объекта WebApplication можно настроить всю инфраструктуру приложения - его конфигурацию, маршруты и так далее. 

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}

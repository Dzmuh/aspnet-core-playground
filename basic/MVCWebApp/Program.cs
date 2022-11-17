namespace MVCWebApp
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
            //
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Получив объект WebApplicationBuilder, у него вызывается метод Build(), который собствено и создает объект WebApplication:
            var app = builder.Build();
            // ⬆ С помощью объекта WebApplication можно настроить всю инфраструктуру приложения - его конфигурацию, маршруты и так далее. 

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Установка обработчика статических файлов.
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

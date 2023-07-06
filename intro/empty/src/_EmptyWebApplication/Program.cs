using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmptyWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Чтобы запустить приложение ASP.NET Core, необходим объект <see cref="IHost"/>, в рамках которого развертывается веб-приложение.
        /// Для создания IHost применяется объект <see cref="IHostBuilder"/>.
        /// </summary>
        /// <remarks>
        /// В статическом методе CreateHostBuilder создается и настраивается IHostBuilder.
        /// Непосредственно создание IHostBuilder производится с помощью метода Host.CreateDefaultBuilder(args).
        /// </remarks>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

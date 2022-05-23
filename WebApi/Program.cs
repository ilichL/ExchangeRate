using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
namespace WebApi
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
                        Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, lc) =>
                {
                    lc.MinimumLevel.Information().WriteTo.Console();
                    lc.MinimumLevel.Warning().WriteTo.File(@"C:\Users\adm\OneDrive\Рабочий стол\TRy\ExchangeRate\ExchangeRate\Logger\log.log");

                }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}

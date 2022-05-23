using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ExchangeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
         .UseSerilog((ctx, lc) =>
         {
             lc.MinimumLevel.Warning().WriteTo.File(@"C:\Users\adm\OneDrive\Рабочий стол\TRy\ExchangeRate\ExchangeRate\Logger\log.log");

         })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });



    }
}

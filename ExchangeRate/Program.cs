using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Serilog;
using Microsoft.AspNetCore.Authentication.Cookies;
using ExchangeRate.Domain.Services;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Data.Entities;
using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Filters;
using ExchangeRate.DataAccess;
using ExchangeRate.Data;

namespace ExchangeRate
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
                        Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, lc) =>
                {
                    lc.MinimumLevel.Information().WriteTo.Console();
                    //lc.MinimumLevel.Debug().WriteTo.CustomSink();??????????
                    lc.MinimumLevel.Warning().WriteTo.File(@"C:\Users\adm\OneDrive\Рабочий стол\TRy\ExchangeRate\ExchangeRate\Logger\log.log");
                
                }) .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}


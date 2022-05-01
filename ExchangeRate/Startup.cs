using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Data;
using ExchangeRate.Data.Entities;
using ExchangeRate.DataAccess;
using ExchangeRate.Domain.Services;
using ExchangeRate.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.SqlServer;

namespace ExchangeRate
{
    public class Startup
    {
        public Startup()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                //.AddJsonFile("emailConfiguration.json")
                .AddInMemoryCollection();
            //.AddTxtConfiguration("123");

            Configuration = configurationBuilder.Build();
        }//System.IO.FileNotFoundException: "The configuration file 'emailConfiguration.json' was not found and is not optional. The expected physical path was 'C:\Users\adm\OneDrive\Рабочий стол\TRy\ExchangeRate\ExchangeRate\bin\Debug\net6.0\emailConfiguration.json'."

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //получаем строку конфигураии для Context
            services.AddDbContext<Context>(opt => opt.UseSqlServer(connectionString));

            // Add services to the container.
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //подключение репозиториев
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Source>, SourceRepository>();
            services.AddScoped<IRepository<Comment>, CommentRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRepository<UserRole>, UserRoleRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            services.AddScoped<IRssService, RssService>();//RssService в коре и в домэйне
            services.AddScoped<IEmailSender, EmailSenderService>();
            services.AddScoped<ISourceService, SourceService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICyrrencyConfigurationService, CyrrencyConfigurationService>();
            //добавить хэндфаер

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/account/login";
                    opt.AccessDeniedPath = "/access-denied";
                });


            services.AddAuthentication();//перед авторизацией иначе не заработает
            services.AddAuthorization();
            services.AddControllersWithViews();//подключение контроллеров с вьюхами(по умолчанию)


     
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"),
                new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer();

            services.AddScoped<CustomFilter>();//подключение(регистрация) фильтра

            /*services.AddControllersWithViews(opt =>
            {//подключает фильтры для всего решения
                //opt.Filters.Add(typeof(SampleResourceFilter));//3 одинаковых способа применения фильтров для всего решения
                //opt.Filters.Add(new SampleResourceFilter());//2й
                //opt.Filters.Add<SampleResourceFilter>();//3й
            });*/

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseHangfireDashboard();
            //hangfire
            //var rssService = serviceProvider.GetRequiredService<ICyrrencyConfigurationService>();//System.InvalidOperationException: "No service for type 'ExchangeRate.Domain.Services.CyrrencyConfigurationService' has been registered."
           // RecurringJob.AddOrUpdate("Aggregation Cyrrencies from rss",
             //   () => rssService.AggregateAllCyrrenciesAsync(),
               // "5 21 */13 * MON-SUN");
                
        }
    }
}

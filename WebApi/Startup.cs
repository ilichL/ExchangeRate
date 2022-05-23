using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Data;
using ExchangeRate.Data.Entities;
using ExchangeRate.DataAccess;
using ExchangeRate.Domain.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //�������� ������ ����������� ��� Context
            services.AddDbContext<Context>(opt => opt.UseSqlServer(connectionString));

            // Add services to the container.
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //����������� ������������
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Source>, SourceRepository>();
            services.AddScoped<IRepository<Comment>, CommentRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRepository<UserRole>, UserRoleRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            services.AddScoped<IRssService, RssService>();//RssService � ���� � � �������
            services.AddScoped<IEmailSender, EmailSenderService>();
            services.AddScoped<ISourceService, SourceService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICyrrencyConfigurationService, CyrrencyConfigurationService>();
            //�������� ��������

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/account/login";
                    opt.AccessDeniedPath = "/access-denied";
                });


            services.AddAuthentication();//����� ������������ ����� �� ����������
            services.AddAuthorization();
            services.AddControllersWithViews();//����������� ������������ � �������(�� ���������)

            /*services.AddControllersWithViews(opt =>
            {//���������� ������� ��� ����� �������
                //opt.Filters.Add(typeof(SampleResourceFilter));//3 ���������� ������� ���������� �������� ��� ����� �������
                //opt.Filters.Add(new SampleResourceFilter());//2�
                //opt.Filters.Add<SampleResourceFilter>();//3�
            });*/

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiFirstAppSample v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("EnableAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}

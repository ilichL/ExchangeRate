using ExchangeApi.Middlewares;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Data;
using ExchangeRate.Data.Entities;
using ExchangeRate.DataAccess;
using ExchangeRate.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace ExchangeApi
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
            services.AddDbContext<Context>(opt
                => opt.UseSqlServer(connectionString));
            
            // Add services to the container.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            
            //подключение репозиториев
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Source>, SourceRepository>();
            services.AddScoped<IRepository<Comment>, CommentRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRepository<UserRole>, UserRoleRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IRepository<RefreshToken>, RefreshTokenRepository>();



            services.AddScoped<IRssService, RssService>();//RssService в коре и в домэйне
            services.AddScoped<IEmailSender, EmailSenderService>();
            services.AddScoped<ISourceService, SourceService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICyrrencyConfigurationService, CyrrencyConfigurationService>();
            //добавить хэндфаер
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"]))
                };
            });

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors(options =>
            {
                options.AddPolicy(name: "EnableAll",
                    policy =>
                    {
                        policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExchangeApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExchangeApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("EnableAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

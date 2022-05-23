﻿using ExchangeRate.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeApi.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next,
           IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IJwtService jwtService, IAccountService accountService)
        {
            var token = context.Request.Headers["Authrorization"]
                .FirstOrDefault()?
                .Split(" ")
                .Last();

            var userId = await jwtService.ValidateJwtToken(token);

            if (userId != null)
            {
                context.Items["User"] = accountService.GetUserById(userId.Value);
            }

            await _next(context);
        }
    }
}

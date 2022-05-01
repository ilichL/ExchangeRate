﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace ExchangeRate.Filters
{
    public class SampleActionFilterAttribute : Attribute, IAsyncActionFilter
    {//проверка на валидность модели
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.ActionArguments["id"] = 42;
            }
            await next();
        }
    }
}

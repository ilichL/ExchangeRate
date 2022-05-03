using ExchangeRate.Core.Interfaces.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExchangeRate.Filters
{
    public class CustomFilter : Attribute, IAsyncResourceFilter
    {//будет присылать письмо пользоваелю при входе в аккаунт
        private readonly IEmailSender _emailSender;
        public CustomFilter(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {//сделать проверку входа через другой девайс через другой сервис
            await _emailSender.SendEmailAsync("3");//то, что мы будем отправлять ползователю
            await next();
        }

    }
}

using ExchangeRate.Core.Interfaces.Data;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace ExchangeRate.Domain.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly string _fromEmail;
        private readonly string _emailPassword;

        private readonly IConfiguration _configuration;
        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
            _fromEmail = _configuration["EmailFrom"];
            _emailPassword = _configuration["EmailPassword"];
        }
        public async Task<bool> SendEmailAsync(string subject, string email, string to)
        {


            return true;
        }
    }
}

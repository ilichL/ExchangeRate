using ExchangeRate.Core.Interfaces.Data;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ExchangeRate.Domain.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly string _fromEmail;//моя почта
        private readonly string _emailPassword;

        private readonly IConfiguration _configuration;
        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
            _fromEmail = _configuration["EmailFrom"];
            _emailPassword = _configuration["EmailPassword"];
        }
        public async Task<bool> SendEmailAsync(string toEmail)
        {

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(_fromEmail, "Exchenge Rate");
            // кому отправляем
            MailAddress to = new MailAddress(toEmail);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Вы авторизовались";
            // текст письма
            m.Body = "<h2>Спасибо за регистрацию !</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient(" smtp.yandex.ru", 25);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(_fromEmail, _emailPassword);
            smtp.EnableSsl = true;
            smtp.Send(m);

            return true;
        }
    }
}

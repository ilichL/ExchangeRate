using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.Interfaces.Data
{
    public interface IEmailSender
    {
        public Task<bool> SendEmailAsync(string subject, string email, string to);
    }
}
/*
  subject - что мы отправляем
  email - на какой имэйл ы отправляем
  to - кому мы отправляем
 */

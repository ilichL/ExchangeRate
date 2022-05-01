using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.SerilogSinks
{
    public class CustomSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        //private readonly IMailService _mailService;

        public CustomSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }
        //файлы или подключение к интернету Dispose
        public void Emit(LogEvent logEvent)
        {//обрабатывает ошибку и расписывает ее
            if (logEvent.Level == LogEventLevel.Fatal)
            {
                var message = logEvent.RenderMessage(_formatProvider);
                //_mailService.Send(message);
            }
        }
    }
}

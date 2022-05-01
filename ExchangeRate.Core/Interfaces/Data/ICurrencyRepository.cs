using ExchangeRate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.Interfaces.Data
{
    public interface ICurrencyRepository : IRepository<Currency>
    {
        //написать кастомные методы для Валют
    }
}

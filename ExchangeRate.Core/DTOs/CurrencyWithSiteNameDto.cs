using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.DTOs
{
    public class CurrencyWithSiteNameDto
    {
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal BuyPrice { get; set; }
        public string BankName { get; set; }
        public DateTime CreationDate { get; set; }
        public string CurrencycType { get; set; }//EUR RUB USD
        public string SiteName { get; set; }
    }
}

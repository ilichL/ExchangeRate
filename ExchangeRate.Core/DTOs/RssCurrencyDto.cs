using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.DTOs
{
    public class RssCurrencyDto
    {//правильные поля?
        public Guid ID { get; set; }
        public decimal EurBuy { get; set; }
        public decimal EurSell { get; set; }
        public decimal RubBuy { get; set; }
        public decimal RubSell { get; set; }
        public decimal UsdBuy { get; set; }
        public decimal UsdSell { get; set; }
        public string BankName { get; set; }
        
    }
}

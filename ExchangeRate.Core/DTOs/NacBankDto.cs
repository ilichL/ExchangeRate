using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.DTOs
{
    public class NacBankDto
    {
        public Guid ID { get; set; }
        public Guid SiteID { get; set; }
        public string BankName { get; set; }
        public decimal EurBuy { get; set; }
        public decimal RubBuy { get; set; }
        public decimal UsdBuy { get; set; }

    }
}

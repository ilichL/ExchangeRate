using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.DTOs
{
    public class SourseGetDto
    {
        public Guid ID { get; set; }
        public string BaseUrl { get; set; }
        public string BankName { get; set; }
        public string EurBuyNode { get; set; }
        public string EurSellNode { get; set; }
        public string RubBuyNode { get; set; }
        public string RubSellNode { get; set; }
        public string UsdBuyNode { get; set; }
        public string UsdSellNode { get; set; }

    }
}

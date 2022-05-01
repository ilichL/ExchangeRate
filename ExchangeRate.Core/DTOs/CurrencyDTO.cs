using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.DTOs
{
    public class CurrencyDTO
    {//нужен для  CurrencyService(Domain)
        public string Name { get; set; }//имя банка валюта
        public decimal EurBuy { get; set; }
        public decimal EurSell { get; set; }
        public decimal RubBuy { get; set; }
        public decimal RubSell { get; set; }
        public decimal UsdBuy { get; set; }
        public decimal UsdSell { get; set; }
        public string BankName { get; set; }
        public DateTime CreationDate { get; set; }
        public string SiteName { get; set; }

        public virtual ICollection<CommentsDTO> CommentDtos { get; set; }

    }
}

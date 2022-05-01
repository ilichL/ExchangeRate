using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Entities
{
    public class Currency : BaseEntity
    {
        public decimal EurBuy { get; set; }
        public decimal EurSell { get; set; }
        public decimal RubBuy { get; set; }
        public decimal RubSell { get; set; }
        public decimal UsdBuy { get; set; }
        public decimal UsdSell { get; set; }

        public string BankName { get; set; }
        public DateTime CreationDate { get; set; }
        public string CurrencycType { get; set; }//EUR RUB USD
        public Source Site { get; set; }
        public Guid SiteID { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

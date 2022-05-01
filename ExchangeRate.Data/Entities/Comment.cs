using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public DateTime CreationDateTime { get; set; }
        public int Rate { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid CurrencyID { get; set; }
        public virtual Currency Currency { get; set; }
    }
}

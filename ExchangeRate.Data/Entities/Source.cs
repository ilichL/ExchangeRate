using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Entities
{
    public class Source : BaseEntity
    {
        public string BaseUrl { get; set; }//ссылка на банк
        public string EurBuyNode { get; set; }//Xpath по которому достаем значение
        public string EurSellNode { get; set; }
        public string RubBuyNode { get; set; }
        public string RubSellNode { get; set; }
        public string UsdBuyNode { get; set; }
        public string UsdSellNode { get; set; }
    }
}

//Xpath будет лежать с сылкой на банк, из сылки достаем название банка
//Xpath будут лежать упорядочеено

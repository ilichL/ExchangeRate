namespace ExchangeRate.Models.Currency
{
    public class CurrencyTableViewModel
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

    }
}

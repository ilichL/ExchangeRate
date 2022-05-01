namespace ExchangeRate.Models.Currency
{
    public class CurrencyCreateModel
    {
        public string Name { get; set; }//имя банка валюта
        public decimal SalePrice { get; set; }
        public decimal BuyPrice { get; set; }
        public string BankName { get; set; }
        public string CurrencycType { get; set; }//EUR RUB USD
    }
}

namespace ExchangeRate.Models.Currency
{
    public class CurrencyListView
    {//для View Index
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal BuyPrice { get; set; }
        public string BankName { get; set; }
        public DateTime CreationDate { get; set; }
        public string CurrencycType { get; set; }//EUR RUB USD

    }
}

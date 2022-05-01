namespace ExchangeRate.Models.Comment
{
    public class CreateComment
    {//delete?
        public Guid UserId { get; set; }
        public string Mail { get; set; }
        public string Text { get; set; }
        public Guid CurrencyId { get; set; }
    }
}

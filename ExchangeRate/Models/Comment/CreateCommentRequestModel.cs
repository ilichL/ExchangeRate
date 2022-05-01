namespace ExchangeRate.Models.Comment
{
    public class CreateCommentRequestModel
    {//CommentController/Create
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public Guid CurrencyID { get; set; }
    }
}

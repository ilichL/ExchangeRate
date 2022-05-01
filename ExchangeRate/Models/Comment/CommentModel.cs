namespace ExchangeRate.Models.Comment
{
    public class CommentModel
    {
        // для VIew
        public Guid Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Text { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}

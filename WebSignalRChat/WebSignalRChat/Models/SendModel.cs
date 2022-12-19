namespace WebSignalRChat.Models
{
    public class SendModel
    {
        public int Id { get; set; }

        public string From { get; set; }
        public string Value { get; set; }

        public DateTime TimeOnly { get; set; }
    }
}

namespace Common.MessengerBot.Telegram.Models
{
    public class MessageInfo
    {
        public string ChatId { get; set; }
        public string Text { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
    }
}
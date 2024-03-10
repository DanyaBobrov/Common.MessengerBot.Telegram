using System.Text.Json.Serialization;

namespace Common.MessengerBot.Telegram.Models.Telegram
{
    public class Message
    {
        [JsonPropertyName("message_id")]
        public long MessageId { get; set; }

        [JsonPropertyName("date")]
        public long DateTimeStamp { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace Common.MessengerBot.Telegram.Models.Telegram.Internal
{
    internal class MessageRequest
    {
        [JsonPropertyName("chat_id")]
        public string ChatId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
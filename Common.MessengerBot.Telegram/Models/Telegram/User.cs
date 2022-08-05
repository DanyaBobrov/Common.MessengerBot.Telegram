using System.Text.Json.Serialization;

namespace Common.MessengerBot.Telegram.Models.Telegram
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("is_bot")]
        public bool IsBot { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
    }
}
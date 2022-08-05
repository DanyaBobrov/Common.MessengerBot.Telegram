using System.Text.Json.Serialization;

namespace Common.MessengerBot.Telegram.Models.Telegram.Internal
{
    internal class BaseResponse<T>
    {
        [JsonPropertyName("ok")]
        public bool Success { get; set; }

        [JsonPropertyName("result")]
        public T Result { get; set; }

        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
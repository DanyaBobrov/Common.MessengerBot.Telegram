using System.Text.Json;

namespace Common.MessengerBot.Telegram.Helpers
{
    internal static class JsonHelper
    {
        private static JsonSerializerOptions serializeOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private static JsonSerializerOptions deserializeOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        public static string ToJsonString(this object obj)
        {
            if (obj == null)
                return string.Empty;

            return JsonSerializer.Serialize(obj, serializeOptions);
        }

        public static T FromJsonString<T>(this string json)
        {
            if (string.IsNullOrEmpty(json))
                return default(T);

            return JsonSerializer.Deserialize<T>(json, deserializeOptions);
        }
    }
}
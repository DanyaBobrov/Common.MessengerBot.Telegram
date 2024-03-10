using Common.MessengerBot.Telegram.Exceptions;
using Common.MessengerBot.Telegram.Helpers;
using Common.MessengerBot.Telegram.Models;
using Common.MessengerBot.Telegram.Models.Telegram;
using Common.MessengerBot.Telegram.Models.Telegram.Internal;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.MessengerBot.Telegram
{
    internal class TelegramBot : ITelegramBot
    {
        private readonly string name;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptionsMonitor<TelegramOptions> telegramOptions;

        public TelegramBot(
            string name,
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<TelegramOptions> telegramOptions)
        {
            this.name = name;
            this.httpClientFactory = httpClientFactory;
            this.telegramOptions = telegramOptions;
        }

        public static string HttpClientName(string name) => $"TelegramBot_{name}";

        public async Task<User> GetMeAsync()
        {
            var options = telegramOptions.Get(name);
            using var httpClient = httpClientFactory.CreateClient(HttpClientName(name));
            using (var response = await httpClient.GetAsync($"/bot{options.Token}/getMe"))
            {
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = responseContent.FromJsonString<BaseResponse<User>>();
                if (!result.Success)
                    throw new TelegramBotException($"The request failed with an error '{result.ErrorCode}'. Description: {result.Description}");

                return result.Result;
            }
        }

        public Task<Message> SendAsync(MessageType messageType, MessageInfo message) => messageType switch
        {
            MessageType.Text => SendMessageAsync(message),
            MessageType.Photo => SendPhotoAsync(message),
            MessageType.Document => SendDocumentAsync(message),
            _ => throw new InvalidEnumArgumentException(messageType.ToString(), (int)messageType, typeof(MessageType))
        };

        public async Task<Message> SendMessageAsync(MessageInfo message)
        {
            if (string.IsNullOrEmpty(message.Text))
                throw new ArgumentNullException(nameof(message.Text));

            var request = new MessageRequest()
            {
                ChatId = message.ChatId,
                Text = message.Text
            };

            var options = telegramOptions.Get(name);
            using var httpClient = httpClientFactory.CreateClient(HttpClientName(name));
            using (var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json"))
            {
                using (var response = await httpClient.PostAsync($"/bot{options.Token}/sendMessage", content))
                {
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = responseContent.FromJsonString<BaseResponse<Message>>();
                    if (!result.Success)
                        throw new TelegramBotException($"The request failed with an error '{result.ErrorCode}'. Description: {result.Description}");

                    return result.Result;
                }
            }
        }

        public async Task<Message> SendPhotoAsync(MessageInfo message)
        {
            if (string.IsNullOrEmpty(message.FileName))
                throw new ArgumentNullException(nameof(message.FileName));
            if (message.File == null)
                throw new ArgumentNullException(nameof(message.File));

            using var streamcontent = new ByteArrayContent(message.File);
            using var stringContent = new StringContent(message.ChatId);

            var options = telegramOptions.Get(name);
            using var httpClient = httpClientFactory.CreateClient(HttpClientName(name));
            using (var content = new MultipartFormDataContent())
            {
                content.Add(streamcontent, "photo", message.FileName);
                content.Add(stringContent, "chat_id");

                using (var response = await httpClient.PostAsync($"/bot{options.Token}/sendPhoto", content))
                {
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = responseContent.FromJsonString<BaseResponse<Message>>();
                    if (!result.Success)
                        throw new TelegramBotException($"The request failed with an error '{result.ErrorCode}'. Description: {result.Description}");

                    return result.Result;
                }
            }
        }

        public async Task<Message> SendDocumentAsync(MessageInfo message)
        {
            if (string.IsNullOrEmpty(message.FileName))
                throw new ArgumentNullException(nameof(message.FileName));
            if (message.File == null)
                throw new ArgumentNullException(nameof(message.File));

            using var streamcontent = new ByteArrayContent(message.File);
            using var stringContent = new StringContent(message.ChatId);

            var options = telegramOptions.Get(name);
            using var httpClient = httpClientFactory.CreateClient(HttpClientName(name));
            using (var content = new MultipartFormDataContent())
            {
                content.Add(streamcontent, "document", message.FileName);
                content.Add(stringContent, "chat_id");

                using (var response = await httpClient.PostAsync($"/bot{options.Token}/sendDocument", content))
                {
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = responseContent.FromJsonString<BaseResponse<Message>>();
                    if (!result.Success)
                        throw new TelegramBotException($"The request failed with an error '{result.ErrorCode}'. Description: {result.Description}");

                    return result.Result;
                }
            }
        }
    }
}
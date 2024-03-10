using Common.MessengerBot.Telegram.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace Common.MessengerBot.Telegram
{
    internal class TelegramBotFactory : ITelegramBotFactory
    {
        private readonly ConcurrentDictionary<string, Lazy<ITelegramBot>> items = new ConcurrentDictionary<string, Lazy<ITelegramBot>>();
        private readonly IServiceProvider serviceProvider;

        public TelegramBotFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ITelegramBot CreateBot(string name = null)
        {
            return items.GetOrAdd(name ?? Options.DefaultName, (name) =>
            {
                var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                var telegramOptions = serviceProvider.GetRequiredService<IOptionsMonitor<TelegramOptions>>();
                return new Lazy<ITelegramBot>(() => new TelegramBot(name, httpClientFactory, telegramOptions));
            }).Value;
        }
    }
}
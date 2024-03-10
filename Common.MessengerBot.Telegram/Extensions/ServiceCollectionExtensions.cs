using Common.MessengerBot.Telegram;
using Common.MessengerBot.Telegram.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration configuration, Action<TelegramBotBuilder> action = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            action?.Invoke(new TelegramBotBuilder(services, Options.Options.DefaultName));

            services.TryAddSingleton<ITelegramBotFactory, TelegramBotFactory>();
            services
                .Configure<TelegramOptions>(Options.Options.DefaultName, configuration)
                .AddHttpClient(TelegramBot.HttpClientName(Options.Options.DefaultName), (client) =>
                {
                    client.BaseAddress = new Uri("https://api.telegram.org");
                });
            return services;
        }

        public static IServiceCollection AddTelegramBot(this IServiceCollection services, string name, IConfiguration configuration, Action<TelegramBotBuilder> action = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            action?.Invoke(new TelegramBotBuilder(services, name));

            services.TryAddSingleton<ITelegramBotFactory, TelegramBotFactory>();
            services
                .Configure<TelegramOptions>(name, configuration)
                .AddHttpClient(TelegramBot.HttpClientName(name), (client) =>
                {
                    client.BaseAddress = new Uri("https://api.telegram.org");
                });
            return services;
        }
    }
}
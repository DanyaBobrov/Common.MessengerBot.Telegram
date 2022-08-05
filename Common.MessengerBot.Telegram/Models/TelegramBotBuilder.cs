using Microsoft.Extensions.DependencyInjection;

namespace Common.MessengerBot.Telegram.Models
{
    public class TelegramBotBuilder
    {
        private readonly IServiceCollection service;
        private readonly string name;

        public TelegramBotBuilder(IServiceCollection service, string name)
        {
            this.service = service;
            this.name = name;
        }

        public void AddHealthCheck()
        {
            service
                .AddHealthChecks()
                .AddCheck<TelegramBotHealthCheck>(name);
        }
    }
}
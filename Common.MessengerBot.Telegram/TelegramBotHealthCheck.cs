using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common.MessengerBot.Telegram
{
    internal class TelegramBotHealthCheck : IHealthCheck
    {
        private readonly ITelegramBotFactory telegramBotFactory;

        public TelegramBotHealthCheck(ITelegramBotFactory telegramBotFactory)
        {
            this.telegramBotFactory = telegramBotFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var telegramBot = telegramBotFactory.CreateBot(context.Registration.Name);
                var user = await (telegramBot as TelegramBot).GetMeAsync();
                return HealthCheckResult.Healthy("Ok");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Error", ex);
            }
        }
    }
}
namespace Common.MessengerBot.Telegram
{
    public interface ITelegramBotFactory
    {
        public ITelegramBot CreateBot(string name = null);
    }
}
using System;

namespace Common.MessengerBot.Telegram.Exceptions
{
    public class TelegramBotException : ApplicationException
    {
        public TelegramBotException() { }
        public TelegramBotException(string message) : base(message) { }
        public TelegramBotException(string message, Exception innerException) : base(message, innerException) { }
    }
}
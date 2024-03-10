using Common.MessengerBot.Telegram.Models;
using Common.MessengerBot.Telegram.Models.Telegram;
using System.Threading.Tasks;

namespace Common.MessengerBot.Telegram
{
    public interface ITelegramBot
    {
        Task<Message> SendAsync(MessageType messageType, MessageInfo message);
        Task<Message> SendMessageAsync(MessageInfo message);
        Task<Message> SendPhotoAsync(MessageInfo message);
        Task<Message> SendDocumentAsync(MessageInfo message);
    }
}
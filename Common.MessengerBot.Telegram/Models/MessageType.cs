namespace Common.MessengerBot.Telegram.Models
{
    public enum MessageType
    {
        /// <summary>
        /// Use this method to send text messages
        /// </summary>
        Text,
        /// <summary>
        /// Use this method to send photos
        /// </summary>
        Photo,
        /// <summary>
        /// Use this method to send general files
        /// </summary>
        Document
    }
}
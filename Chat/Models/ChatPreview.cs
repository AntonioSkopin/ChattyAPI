using System;

namespace ChattyAPI.Chat.Models
{
    public class ChatPreview
    {
        public Guid chat_gd { get; set; }
        public string username { get; set; }
        public string message { get; set; }
        public DateTime date_sent { get; set; }
    }
}
using System;

namespace ChattyAPI.Chat.Models
{
    public class Message
    {
        public Guid gd { get; set; }
        public string message { get; set; }
        public Guid sent_by_gd { get; set; }
        public Guid chat_gd { get; set; }
        public DateTime date_sent { get; set; }
    }
}
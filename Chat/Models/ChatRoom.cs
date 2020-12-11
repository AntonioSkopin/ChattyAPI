using System;

namespace ChattyAPI.Chat.Models
{
    public class ChatRoom
    {
        public Guid gd { get; set; }
        public Guid users_gd { get; set; }
        public DateTime date_created { get; set; }
    }
}
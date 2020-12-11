using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChattyAPI.Chat.Models;

namespace ChattyAPI.Chat.Contracts
{
    public interface IChatManager
    {
        public Task<Guid> CreateChat(ChatParticipants chatParticipants);
        public Task DeleteChat(Guid chat_gd);
        public Task SendMessage(Message message);
        public Task<List<Message>> GetMessagesOfChat(Guid chat_gd);
        public Task<List<ChatPreview>> GetChatsOfUser(Guid user_gd);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChattyAPI.Base.Managers;
using ChattyAPI.Chat.Contracts;
using ChattyAPI.Chat.Models;
using Microsoft.Extensions.Configuration;

namespace ChattyAPI.Chat.Managers
{
    public class ChatManager : BaseManager, IChatManager
    {
        private readonly IConfiguration _configuration;

        public ChatManager(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<Guid> CreateChat(ChatParticipants chatParticipants)
        {
            // Query to add the chat participants
            var addParticipantsQuery =
            @"
                insert into chat_participants
                values
                (
                    @_gd, @_user1_gd, @_user2_gd
                )
            ";

            // Generate chatGD
            var chatGD = GenerateGd();
            var chatParticipantsGD = GenerateGd();

            // Execute addParticipantsQuery
            await PostQuery(addParticipantsQuery, new
            {
                _gd = chatParticipantsGD,
                _user1_gd = chatParticipants.user1_gd,
                _user2_gd = chatParticipants.user2_gd
            });

            // Query to create the chat
            var createChatQuery =
            @"
                insert into chatroom
                values
                (
                    @_gd, @_users_gd, GETDATE()
                )
            ";

            // Execute createChatQuery
            await PostQuery(createChatQuery, new
            {
                _gd = chatGD,
                _users_gd = chatParticipantsGD
            });

            return chatGD;
        }

        public async Task SendMessage(Message message)
        {
            // Query to send a message
            var sendMessageQuery =
            @"
                insert into message
                values
                (
                    NEWID(), @_message, @_sent_by_gd, @_chat_gd, GETDATE()
                )
            ";

            // Execute sendMessageQuery
            await PostQuery(sendMessageQuery, new
            {
                _message = message.message,
                _sent_by_gd = message.sent_by_gd,
                _chat_gd = message.chat_gd
            });
        }

        public async Task<List<Message>> GetMessagesOfChat(Guid chat_gd)
        {
            // Query to get all messages of a chat
            var getMessagesOfChatQuery =
            @"
                select * from message
                where chat_gd = @_chat_gd
                order by date_sent asc
            ";

            // Execute getMessagesOfChatQuery and return result
            return await GetManyQuery<Message>(getMessagesOfChatQuery, new
            {
                _chat_gd = chat_gd
            });
        }

        public async Task DeleteChat(Guid chat_gd)
        {
            // Query to delete a chat
            var deleteChatQuery =
            @"
                delete from chatroom
                where gd = @_chat_gd
            ";

            // Execute query
            await DeleteQuery(deleteChatQuery, new
            {
                _chat_gd = chat_gd
            });
        }

        public async Task<List<ChatPreview>> GetChatsOfUser(Guid user_gd)
        {
            // Query to get all chats of user
            var getChatRoomsQuery =
            @"
                select distinct chat_gd from message
            ";

            List<Guid> chatRooms = await GetManyQuery<Guid>(getChatRoomsQuery);
            List<ChatPreview> chatsOfUser = new List<ChatPreview>();

            foreach (Guid chatRoomGd in chatRooms)
            {
                // Query to get all chats of an user
                var getChatsOfUserQuery =
                @"
                    select usr.UserName, msg.* from message msg
                    left join AspNetUsers usr on msg.sent_by_gd = usr.gd
                    where usr.gd != @_user_gd
                    and chat_gd = @_chat_gd
                    order by date_sent
                ";

                var result = await GetQuery<ChatPreview>(getChatsOfUserQuery, new
                {
                    _user_gd = user_gd,
                    _chat_gd = chatRoomGd
                });

                chatsOfUser.Add(result);
            }

            return chatsOfUser;
        }
    }
}
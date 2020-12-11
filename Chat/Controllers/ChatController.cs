using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChattyAPI.Base.Models;
using ChattyAPI.Chat.Contracts;
using ChattyAPI.Chat.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChattyAPI.Chat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatManager _chatManager;

        public ChatController(IChatManager chatManager)
        {
            _chatManager = chatManager;
        }

        /* POST API'S */
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateChat([FromBody] ChatParticipants chatParticipants)
        {
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new Response { Status = "Error!", Message = "Something went wrong" });
            }
            var chatGD = await _chatManager.CreateChat(chatParticipants);
            return Ok(chatGD);
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] Message message)
        {
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new Response { Status = "Error!", Message = "Something went wrong" });
            }
            await _chatManager.SendMessage(message);
            return Ok();
        }
        /* POST API'S */

        /* GET API'S */
        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetMessagesOfChat(Guid chat_gd)
        {
            var messagesOfChat = await _chatManager.GetMessagesOfChat(chat_gd);
            return Ok(messagesOfChat);
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatPreview>>> GetChatsOfUser(Guid user_gd)
        {
            var userChatRooms = await _chatManager.GetChatsOfUser(user_gd);
            return Ok(userChatRooms);
        }
        /* GET API'S */

        /* DELETE API'S */
        [HttpDelete]
        public async Task<ActionResult> DeleteChat(Guid chat_gd)
        {
            await _chatManager.DeleteChat(chat_gd);
            return Ok();
        }
        /* DELETE API'S */

        /* PUT API'S */
        /* PUT API'S */
    }
}
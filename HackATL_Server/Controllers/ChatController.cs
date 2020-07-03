using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackATL_Server.Helper;
using HackATL_Server.Models.Model.Chat_related;
using HackATL_Server.Models.Repository;
using HackATL_Server.Models.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackATL_Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IUserService _userService;
        private IChatService _chatService; 
        private readonly AppSettings _appSettings;
        public ChatController(
            IUserService userService,
            IOptions<AppSettings> appSettings,
            IChatService chatService)
        {
            //_userService = userService;
            //_appSettings = appSettings.Value;
            _chatService = chatService;


        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}/getchatList")]
        public ActionResult<UserChat_LogList> GetRoomList(string id)
        {
            var user_chatroomList = _chatService.GetChatRoomList(id);
            return user_chatroomList;

        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("addchat")]
        public ActionResult<ChatRoom_participants> AddRoom([FromBody]AddChatRoom input)
        {
            if(input != null)
            {
                var added = _chatService.AddInitiate(input);
                ChatRoom_participants chatroom = new ChatRoom_participants();
                chatroom = added;
                if (chatroom == null)
                {
                    return BadRequest();
                }
                return Ok(chatroom);

            }
            else
            {
                return BadRequest();
            }
            
        }
    }
}

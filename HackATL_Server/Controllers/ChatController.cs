using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackATL_Server.Models.Model.MongoDatabase.Chats;
using HackATL_Server.Models.Model_General;
using HackATL_Server.Models.Model_Http.Chat;
using HackATL_Server.Repos.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackATL_Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private IChatService _ChatService;
        public ChatController(IChatService ChatService)
        {
            _ChatService = ChatService; 
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Chatroom> CreateChat([FromBody]Chat_Create create)
        {
            Chatroom chatroom = new Chatroom();
            chatroom = _ChatService.CreateChat(create);
            if(chatroom == null)
            {
                return BadRequest();
            }
            return Ok(chatroom);
            

        }

        [HttpPost("getsinglechat")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Chatroom> GetChat_Single([FromBody] StringClass chatroomID)
        {
            Chatroom chatroom = new Chatroom();
            chatroom = _ChatService.GetChat(chatroomID.str);
            if (chatroom == null)
            {
                return BadRequest();
            }
            return Ok(chatroom);
        }

        [HttpPost("addmember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Chatroom>> AddMember([FromBody] Chat_AddMembers add)
        {
            Chat_MemberInfo chatroom = new Chat_MemberInfo();

            Task<Chat_MemberInfo> task = _ChatService.AddMembers(add);
            chatroom = await task;
    
            if (chatroom == null)
            {
                return BadRequest();
            }
            return Ok(chatroom);
        }

        [HttpPost("getmemberinfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Chatroom> GetMemberInfo([FromBody] StringClass model)
        {
            Chat_MemberInfo memberInfo = new Chat_MemberInfo();
            memberInfo = _ChatService.GetMembersInfo_Single(model.str);
            if (memberInfo == null)
            {
                return BadRequest();
            }
            return Ok(memberInfo);
        }

        [HttpPost("getmembersinfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Chatroom>> GetMembersInfo([FromBody] ListClass_String model)
        {
            List<Chat_MemberInfo> membersInfo = new List<Chat_MemberInfo>();
            membersInfo = _ChatService.GetMembersInfo_All(model.list);
            if (membersInfo == null)
            {
                return BadRequest();
            }
            return Ok(membersInfo);
        }

        [HttpPost("getmembersinfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Boolean> GetMembersInfo([FromBody] Chat_Leave model)
        {
            
            var status = _ChatService.LeaveChat(model);
            if (status)
            {
                return BadRequest();
            }
            return Ok(status);
        }









    }
}

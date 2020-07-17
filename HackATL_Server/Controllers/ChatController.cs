using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackATL_Server.Helper;
using HackATL_Server.Models.Model.Chat_related;
using HackATL_Server.Models.Model.MongoDatabase.Chats;
using HackATL_Server.Models.Model.MongoDatabase.Users;
using HackATL_Server.Models.Model_Http.Chat;
using HackATL_Server.Models.Repository;
using HackATL_Server.Models.Repository.Interfaces;
using HackATL_Server.Repository.Interfaces_MongoDB;
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
        private IUserService_md _userService;
        private IChatService_md _chatService; 
        private readonly AppSettings _appSettings;
        public ChatController(
            IUserService_md userService,
            IOptions<AppSettings> appSettings,
            IChatService_md chatService)
        {
            //_userService = userService;
            //_appSettings = appSettings.Value;
            _chatService = chatService;


        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getroom")]
        public ActionResult<List<Chatroom>> GetRooms([FromBody]string uID)
        {
            List<Chatroom> rooms = new List<Chatroom>();
            rooms = _chatService.GetChatrooms(uID);
            if (rooms == null){
                return BadRequest();
            }
            return Ok(rooms);
            

        }

        
        


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("addmember")]
        public ActionResult AddMember([FromBody] AddDelete model)
        {
            var status = _chatService.AddMember(model.uID, model.chatID);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
            


        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("create")]
        public ActionResult CreateRoom([FromBody]List<string>members)
        {
            
            List<User_Personal> personals = _chatService.CreateChatroom(members);
            if (personals != null)
                return Ok(personals);
            return BadRequest();
            
        }


        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("getmembers")]
        public ActionResult GetMembers([FromBody] List<string> members)
        {

            List<User_Personal> personals = _chatService.CreateChatroom(members);
            if (personals != null)
                return Ok(personals);
            return BadRequest();

        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("delete")]
        public ActionResult DeleteRoom([FromBody]AddDelete model)
        {
            var status = _chatService.DeleteChatroom(model.uID, model.chatID);
            if (status)
            {
                return Ok();
            }
            return BadRequest();


        }
    }
}

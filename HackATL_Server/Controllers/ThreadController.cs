using System;
using HackATL_Server.Models.Model.MongoDatabase.Forums;
using HackATL_Server.Models.Model_General;
using HackATL_Server.Models.Model_Http.Forum;
using HackATL_Server.Repos.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackATL_Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ThreadController : ControllerBase
    {
        private IThreadService _ThreadService;
        public ThreadController(IThreadService ThreadService)
        {
            _ThreadService = ThreadService;
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Thread> CreateThread([FromBody] Thread_Detail create)
        {
            Thread thread = new Thread();
            thread = _ThreadService.CreateThread(create);
            if(thread == null)
                return BadRequest();
            return Ok(thread);

        }

        [HttpPost("deletethread")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Thread> DeleteThread([FromBody] StringClass model)
        {
 
            var status = _ThreadService.DeleteThread(model.str);
            if (status)
                return BadRequest();
            return Ok(status);

        }

        [HttpPost("edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Thread> EditThread([FromBody] Thread_Detail edit)
        {
            var status = _ThreadService.EditThread(edit);
            if (status)
                return BadRequest();
            return Ok(status);

        }

        [HttpPost("addcomment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Thread> AddComment([FromBody] Thread_Comment comment)
        {
            Thread_Comment thread = new Thread_Comment();
            thread = _ThreadService.Thread_AddComment(comment);
            if (thread == null)
                return BadRequest();
            return Ok(thread);

        }

        [HttpPost("addremovefavorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Thread> AddRemoveFavorite([FromBody] Thread_Favorite fav)
        {
            var status = _ThreadService.Thread_AddRemoveFavorite(fav);
            if (status)
                return BadRequest();
            return Ok(status);

        }
    }
}

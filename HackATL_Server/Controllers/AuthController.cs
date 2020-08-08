using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackATL_Server.Models.Model.MongoDatabase.Users;
using HackATL_Server.Models.Model_General;
using HackATL_Server.Models.Model_Http.User;
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
    public class AuthController : ControllerBase
    {
        private IAuthService _AuthService; 
        public AuthController(IAuthService AuthService)
        {
            _AuthService = AuthService;

        }
        [AllowAnonymous]
        [HttpPost("namecheck")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult NameCheck([FromBody] StringClass model)
        {
            
            var status = _AuthService.NameCheck(model.str);
            if (status)
            {
                return Ok();
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Register ([FromBody] User_Register register)
        {
            User user = new User();
            user = _AuthService.Register(register);
            if (user == null)
            {
                return BadRequest();
            }
            
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Authenticate([FromBody] User_Authenticate authenticate)
        {
            User user = new User();
            user = _AuthService.Authenticate(authenticate);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HackATL_Server.Helper;
using HackATL_Server.Models.Model;
using HackATL_Server.Models.Model.authentication;
using HackATL_Server.Models.Model.Chat_related;
using HackATL_Server.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackATL_Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository UserRepository;
        private IMapper _mapper;
        private IUserService _userService;
        
        private readonly AppSettings _appSettings;
        public UserController(
            IUserRepository userRepository,
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings

            )
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            UserRepository = userRepository;
        }
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<User>> List()
        {
            return _userService.GetAll().ToList();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("GetUsersPublic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PublicModel>> List_Public()
        {
            return _userService.GetAll_Public().ToList();
        }

        [AllowAnonymous]
        [HttpPost("check")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult NameCheck(string username)
        {
            bool status = _userService.Check(username);
            if (!status)
                return Ok();
            else
            {
                return BadRequest();
            }       
        }




        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserModel> GetUser(string id)
        {
            var user = _userService.GetById(id);
            var model = _mapper.Map<UserModel>(user);
            return model;

            //User user = UserRepository.Get(id);

            //if (user == null)
            //    return NotFound();

            //return user;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Register([FromBody]RegisterModel model)
        {
            var user = _mapper.Map<User>(model);
            
            try
            {
                // create user
                
                var output  = _userService.Create(user, model.Password);
                UserRepository.Add(user);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

            //UserRepository.Add(user);
            //return CreatedAtAction(nameof(GetUser), new { user.Id }, user);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserOutput> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            //auth successful
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.Id.ToString()),
            //      //  new Claim(ClaimTypes.Role, user.Role.ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var tokenString = tokenHandler.WriteToken(token);
            //var tokenString = "";
            UserOutput output = new UserOutput
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Token = tokenString

            };
            return Ok(output);
            // return basic user info and authentication token
            //return Ok(new
            //{
            //    Id = user.Id,
            //    Username = user.Username,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Token = tokenString

            //});
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(string id, [FromBody]UpdateModel model)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.Id = id;
            var temp = _userService.Authenticate(user.Username, model.Password);
            if(temp == null)
            {
                return BadRequest();
            }

            try
            {
                // update user 
                _userService.Update(user, model.Password);
                return NoContent();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
        //public ActionResult Edit([FromBody] User user)
        //{
        //    try
        //    {
        //        UserRepository.Update(user);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest("Error while editing item");
        //    }
         //   return NoContent();
        //}

  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _userService.Delete(id);
            return Ok();
        }
        //public ActionResult Delete(string id)
        //{
        //    User user = UserRepository.Remove(id);

        //    if (user == null)
        //        return NotFound();

        //    return Ok();
        //}

    }
}

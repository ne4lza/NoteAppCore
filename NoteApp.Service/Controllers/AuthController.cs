using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Service.Context;
using NoteApp.Service.DTO.User;
using NoteApp.Service.Models;

namespace NoteApp.Service.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly NoteAppDbContext _context;

        public AuthController(NoteAppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public IActionResult Login(AuthUserDto authUserDto)
        {
            var query = _context.TBL_Users.Where(
                x => x.UserUserName == authUserDto.UserUserName
                &&
                x.UserPassword == authUserDto.UserPassword
                ).FirstOrDefault();

            if (query != null)
            {
                HttpContext.Session.SetString("UserName", query.UserUserName);
                HttpContext.Session.SetInt32("UserId", query.Id);

                SessionData sessionData = new SessionData
                {
                    Id = query.Id,
                    Name = query.UserUserName,
                };
                return Ok(sessionData);
            }
           return NotFound();

        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(AddUserDto addUserDto) 
        {
            var existUser = _context.TBL_Users.Where(x=>x.UserUserName == addUserDto.UserUserName).FirstOrDefault();
            if(existUser != null) 
            {
                return BadRequest();
            }
            else
            {
                User user = new User
                {
                    UserCreatedDate = DateTime.Now,
                    UserName = addUserDto.UserName,
                    UserUserName = addUserDto.UserName,
                    UserLastName = addUserDto.UserName,
                    UserPassword = addUserDto.UserName
                    
                };
                _context.TBL_Users.Add(user);
                _context.SaveChanges();
                return Ok(user);
            }
        }
    }
}

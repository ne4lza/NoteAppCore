using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Service.Context;
using NoteApp.Service.DTO.User;

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
        [HttpPost]
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
            }
            return Ok();

        }
    }
}

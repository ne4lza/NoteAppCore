using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NoteApp.Service.DTO.User;
using NoteApp.Service.Models;
using NoteApp.WebUi.Models;
using System.Diagnostics;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace NoteApp.WebUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string User_UserName, string User_Password)
        {
            var authDto = new AuthUserDto
            {
                UserUserName = User_UserName,
                UserPassword = User_Password
            };
            var json = JsonConvert.SerializeObject(authDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:7067/api/auth", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var sessionData = JsonConvert.DeserializeObject<SessionData>(responseData);

                // Oturum bilgilerini saklamak için uygun bir yöntem kullanýn, örneðin HttpContext.Session
                HttpContext.Session.SetString("UserName", sessionData.Name);
                HttpContext.Session.SetInt32("UserId", sessionData.Id);

                // Baþarýlý giriþ
                return RedirectToAction("Index", "Main");
            }
            else
            {
                // Baþarýsýz giriþ
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace NoteApp.WebUi.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

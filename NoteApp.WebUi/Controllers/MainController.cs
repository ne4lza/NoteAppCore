using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NoteApp.Service.Models;
using NuGet.Packaging.Signing;
using System.Net.Http.Headers;
using System.Text;

namespace NoteApp.WebUi.Controllers
{
    public class MainController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MainController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7068/api/notes/List/{userId}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Note> notes = JsonConvert.DeserializeObject<List<Note>>(content);
                return View(notes);
            }
            else
            {
                // İstek başarısız olduysa burada bir hata işleme mekanizması ekleyebilirsiniz
                throw new HttpRequestException("Error: Unable to retrieve foods");
            }
        }
        [HttpGet]
        public IActionResult AddNote()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNote(Note note)
        {
            note.UserId = (int)HttpContext.Session.GetInt32("UserId");
            note.CreatedDate = DateTime.Now;
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:7068/api/notes/AddNote", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateNote(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7068/api/notes/{id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Note note = JsonConvert.DeserializeObject<Note>(content);
                return View(note);
            }
            else
            {
                // İstek başarısız olduysa burada bir hata işleme mekanizması ekleyebilirsiniz
                throw new HttpRequestException("Error: Unable to retrieve foods");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNote(Note note)
        {
            note.UserId = (int)HttpContext.Session.GetInt32("UserId");
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:7068/api/notes/UpdateNote", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> RemoveNote(int id)
        {
            var json = JsonConvert.SerializeObject(id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7068/api/notes/RemoveNote/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                return View();
            }

        }
    }
}

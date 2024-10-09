using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using futblog.Models;

namespace futblog.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener todos los posts desde la API
            var posts = await _httpClient.GetFromJsonAsync<Post[]>("http://localhost:5239/api/Posts");
            var users = await _httpClient.GetFromJsonAsync<User[]>("http://localhost:5239/api/Users");
            ViewBag.userstotal = users.Count();
            return View(posts);
        }
    }
}

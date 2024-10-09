using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using futblog.Models;

namespace futblog.Controllers
{
    public class AuthController : Controller
    {
        // GET: AuthController
        private readonly HttpClient _httpClient;

        public AuthController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5239/api/")
            };
        }

        // GET: Auth/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Auth/Register
        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Auth/register", userRegisterDto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Error en el registro.");
            return View(userRegisterDto);
        }

        // GET: Auth/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        public async Task<ActionResult> Login(UserLoginDto userLoginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Auth/login", userLoginDto);

            if (response.IsSuccessStatusCode)
            {
                // Aquí podrías manejar la sesión del usuario
                return RedirectToAction("Index", "Home"); // Redirige a la página principal o donde desees
            }

            ModelState.AddModelError("", "Credenciales incorrectas.");
            return View(userLoginDto);
        }

    }
}

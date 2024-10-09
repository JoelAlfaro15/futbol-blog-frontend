using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using futblog.Models;
using System.IO;
using System;

namespace futblog.Controllers
{
    public class PostsController : Controller
    {
        private readonly HttpClient _httpClient;

        public PostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var posts = await _httpClient.GetFromJsonAsync<Post[]>("http://localhost:5239/api/Posts");
            return View(posts);
        }

        // GET: Posts/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _httpClient.GetFromJsonAsync<Category[]>("http://localhost:5239/api/Categories");
            ViewBag.Categories = categories;
            ViewBag.CategoriesCount = categories.Count();
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _httpClient.GetFromJsonAsync<Category[]>("http://localhost:5239/api/Categories");
            Post post = await _httpClient.GetFromJsonAsync<Post>("http://localhost:5239/api/Posts/" + id);
            Console.WriteLine(post.User.Username);
            if (post == null)
            {
                return NotFound(); // Manejo de error si no se encuentra el post
            }

            ViewBag.Categories = categories; // Pasar categorías a la vista
            return View(post); // Pasar el post a la vista
        }


        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostDTO postDto)
        {
            if (!ModelState.IsValid)
            {
                // Si hay errores de validación, volvemos a la vista con el modelo actual
                var categoriess = await _httpClient.GetFromJsonAsync<Category[]>("http://localhost:5239/api/Categories");
                ViewBag.Categories = categoriess;
                return View(postDto);
            }

            // Realiza la petición POST enviando el PostDTO al cuerpo de la solicitud
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5239/api/Posts/CreatePost", postDto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al crear el post. Inténtalo de nuevo.");
                var categories = await _httpClient.GetFromJsonAsync<Category[]>("http://localhost:5239/api/Categories");
                ViewBag.Categories = categories;
                return View(postDto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Post post)
        {
            
            Post pp = await _httpClient.GetFromJsonAsync<Post>("http://localhost:5239/api/Posts/" + post.Id);
            pp.Title = post.Title;
            pp.Body = post.Body;
            pp.CategoryId = post.CategoryId;

            Console.WriteLine(pp.Category.Name);
            Console.WriteLine(pp.User.Username);
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:5239/api/Posts/update/{pp.Id}", pp);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home"); // Redirige a la lista de publicaciones
            }
            
            // Si llegamos aquí, algo falló. Vuelve a mostrar la vista de edición
            ViewBag.Categories = await _httpClient.GetFromJsonAsync<Category[]>("http://localhost:5239/api/Categories");
            return View("Edit", post);
        }

    }
}

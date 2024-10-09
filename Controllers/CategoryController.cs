using futblog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

public class CategoryController : Controller
{
    private readonly HttpClient _httpClient;

    public CategoryController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _httpClient.GetFromJsonAsync<Category[]>("http://localhost:5239/api/Categories");
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5239/api/Categories", category);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Error al crear la categoría.");
        }
        
        return View(category);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _httpClient.GetFromJsonAsync<Category>($"http://localhost:5239/api/Categories/{id}");
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5239/api/Categories/{category.Id}", category);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Error al actualizar la categoría.");
        }
        
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Category category)
    {
        var response = await _httpClient.PutAsJsonAsync($"http://localhost:5239/api/Categories/{category.Id}", category);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View("Edit", category);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"http://localhost:5239/api/Categories/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return BadRequest();
    }
}

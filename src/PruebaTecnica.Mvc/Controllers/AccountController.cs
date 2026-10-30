using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Mvc.Models.Dtos.Auth.Responses;
using PruebaTecnica.Mvc.Models.ViewModels;

namespace PruebaTecnica.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _httpClient.PostAsJsonAsync("api/auth/login", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
                return View(model);
            }

            var resultado = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (resultado != null)
            {
                HttpContext.Session.SetString("UserName", resultado.Username);
                HttpContext.Session.SetString("UserRol", resultado.Rol);
                HttpContext.Session.SetInt32("UserId", resultado.IdUsuario);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // 1. Limpiar las variables de sesión
            HttpContext.Session.Clear();

            // 2. Destruir la cookie de sesión del navegador
            if (Request.Cookies[".AspNetCore.Session"] != null)
            {
                Response.Cookies.Delete(".AspNetCore.Session");
            }

            // 3. Redirigir al Login
            return RedirectToAction("Login", "Account");
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnica.Mvc.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Si la sesión del usuario ya no existe, redirigir al login inmediatamente
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        // Acción que captura y gestiona los errores no controlados del sistema
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Puedes registrar el error en los logs si lo deseas
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Mvc.Models.Dtos.Productos.Requests;
using PruebaTecnica.Mvc.Models.Dtos.Productos.Responses;

namespace PruebaTecnica.Mvc.Controllers
{
    public class ProductosController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        private bool EsAdmin() => HttpContext.Session.GetString("UserRol") == "Administrador";

        [HttpGet]
        public async Task<IActionResult> Index(string? codigo)
        {
            if (!EsAdmin()) return RedirectToAction("Index", "Ventas");

            var productos = await _httpClient.GetFromJsonAsync<IEnumerable<ProductoResponse>>($"api/productos?codigo={codigo}");
            ViewBag.FiltroCodigo = codigo;
            return View(productos ?? []);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoCreateRequest request)
        {
            if (!EsAdmin()) return Unauthorized();

            // Asignamos el ID del usuario logueado en la sesión
            request.IdUsuarioCreacion = HttpContext.Session.GetInt32("UserId") ?? 1;

            // Envía la petición POST hacia la API de productos
            var response = await _httpClient.PostAsJsonAsync("api/productos", request);

            if (response.IsSuccessStatusCode)
            {
                // Retornamos OK para que AJAX detecte el éxito y cierre el modal correctamente
                return Ok(new { success = true });
            }

            return BadRequest("No se pudo crear el producto en la API.");
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProductoCreateRequest request)
        {
            if (!EsAdmin()) return Unauthorized();

            var response = await _httpClient.PutAsJsonAsync($"api/productos/{id}", request);

            if (response.IsSuccessStatusCode)
            {
                // Retornamos OK para que AJAX detecte el éxito y no falle
                return Ok(new { success = true });
            }

            return BadRequest("No se pudo actualizar en la API");
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (!EsAdmin()) return Unauthorized();

            // Envía la petición DELETE hacia la API de productos
            var response = await _httpClient.DeleteAsync($"api/productos/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Retornamos OK para que AJAX detecte el éxito y recargue la tabla
                return Ok(new { success = true });
            }

            return BadRequest("No se pudo eliminar el producto en la API.");
        }
    }
}
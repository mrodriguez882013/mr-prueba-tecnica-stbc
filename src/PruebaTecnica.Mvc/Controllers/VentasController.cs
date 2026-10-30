using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Mvc.Models.Dtos.Ventas.Requests;
using PruebaTecnica.Mvc.Models.Dtos.Ventas.Responses;

namespace PruebaTecnica.Mvc.Controllers
{
    public class VentasController : Controller
    {
        private readonly HttpClient _httpClient;

        public VentasController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        [HttpGet]
        public async Task<IActionResult> BuscarProductoPorCodigo(string codigo)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/productos?codigo={codigo}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return Content(json, "application/json");
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al conectar con la API");
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                return RedirectToAction("Login", "Account");

            ViewBag.Vendedor = HttpContext.Session.GetString("UserName");
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("UserId") ?? 1;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] VentaCreateRequest request)
        {
            request.Vendedor = HttpContext.Session.GetString("UserName") ?? "Operador";
            request.IdUsuarioCreacion = HttpContext.Session.GetInt32("UserId") ?? 1;

            var response = await _httpClient.PostAsJsonAsync("api/ventas", request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return BadRequest(new { message = error });
            }

            return Ok(new { success = true, message = "Venta registrada con éxito." });
        }

        [HttpGet]
        public async Task<IActionResult> Reporte(DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (HttpContext.Session.GetString("UserRol") != "Administrador")
                return RedirectToAction("Index");

            string query = $"api/ventas/reporte?";
            if (fechaInicio.HasValue) query += $"fechaInicio={fechaInicio:yyyy-MM-dd}&";
            if (fechaFin.HasValue) query += $"fechaFin={fechaFin:yyyy-MM-dd}";

            var reporte = await _httpClient.GetFromJsonAsync<IEnumerable<VentaReporteResponse>>(query);
            return View(reporte ?? []);
        }

        [HttpGet]
        public async Task<IActionResult> ExportarExcel(DateTime? fechaInicio, DateTime? fechaFin)
        {
            string query = $"api/ventas/exportar-excel?";
            if (fechaInicio.HasValue) query += $"fechaInicio={fechaInicio.Value:yyyy-MM-dd}&";
            if (fechaFin.HasValue) query += $"fechaFin={fechaFin.Value:yyyy-MM-dd}";

            var response = await _httpClient.GetAsync(query);
            if (!response.IsSuccessStatusCode)
                return NotFound("No se pudo generar el archivo Excel.");

            var fileBytes = await response.Content.ReadAsByteArrayAsync();
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReporteVentas_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        [HttpGet]
        public async Task<IActionResult> ExportarPdf(DateTime? fechaInicio, DateTime? fechaFin)
        {
            string query = $"api/ventas/exportar-pdf?";
            if (fechaInicio.HasValue) query += $"fechaInicio={fechaInicio.Value:yyyy-MM-dd}&";
            if (fechaFin.HasValue) query += $"fechaFin={fechaFin.Value:yyyy-MM-dd}";

            var response = await _httpClient.GetAsync(query);
            if (!response.IsSuccessStatusCode)
                return NotFound("No se pudo generar el archivo PDF.");

            var fileBytes = await response.Content.ReadAsByteArrayAsync();

            return File(fileBytes, "application/pdf", $"ReporteVentas_{DateTime.Now:yyyyMMdd}.pdf");
        }
    }
}
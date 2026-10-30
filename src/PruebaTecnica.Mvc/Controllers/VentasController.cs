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
            try
            {
                // Validación previa opcional para evitar enviar rangos incongruentes a la API
                if (fechaInicio.HasValue && fechaFin.HasValue && fechaFin.Value < fechaInicio.Value)
                {
                    TempData["ErrorMessage"] = "La fecha fin no puede ser anterior a la fecha de inicio.";
                    return View(new List<VentaReporteResponse>());
                }

                string url = $"api/ventas/reporte?fechaInicio={fechaInicio?.ToString("yyyy-MM-dd")}&fechaFin={fechaFin?.ToString("yyyy-MM-dd")}";

                // Usamos GetAsync para evaluar el código de estado sin lanzar excepciones automáticas
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    // Si la API responde con 400 Bad Request u otro error, capturamos el flujo
                    TempData["ErrorMessage"] = "La API rechazó la solicitud. Verifique que el rango de fechas sea válido.";
                    return View(new List<VentaReporteResponse>());
                }

                var reporte = await response.Content.ReadFromJsonAsync<IEnumerable<VentaReporteResponse>>();
                return View(reporte ?? new List<VentaReporteResponse>());
            }
            catch (Exception ex)
            {
                // Captura cualquier caída de red o error inesperado evitando la pantalla de depuración
                TempData["ErrorMessage"] = "Error de conexión al intentar obtener el reporte corporativo.";
                return View(new List<VentaReporteResponse>());
            }
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
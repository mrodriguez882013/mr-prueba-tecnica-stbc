using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Api.Dtos.Ventas.Requests;
using PruebaTecnica.Api.Dtos.Ventas.Responses;
using PruebaTecnica.Api.Services.Interfaces;

namespace PruebaTecnica.Api.Controllers
{
    [ApiController]
    [Route("api/ventas")]
    [Produces("application/json")]
    [Tags("3. Administración de Ventas y Transacciones")]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        /// <summary>Registra una nueva venta de forma transaccional usando TVP y actualiza stock.</summary>
        /// <param name="request">Estructura con los datos del vendedor y el detalle de los productos.</param>
        /// <param name="cancellationToken">Token de cancelación de la solicitud.</param>
        [HttpPost]
        [ProducesResponseType(typeof(VentaResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrarVenta(
            [FromBody] VentaCreateRequest request,
            CancellationToken cancellationToken)
        {
            var ventaResponse = await _ventaService.RegistrarVentaAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, ventaResponse);
        }

        /// <summary>Obtiene un reporte detallado de las ventas realizadas filtradas por rango de fechas.</summary>
        /// <param name="fechaInicio">Fecha de inicio opcional (formato YYYY-MM-DD).</param>
        /// <param name="fechaFin">Fecha de fin opcional (formato YYYY-MM-DD).</param>
        /// <param name="cancellationToken">Token de cancelación de la solicitud.</param>
        [HttpGet("reporte")]
        [ProducesResponseType(typeof(IEnumerable<VentaReporteResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerReporte(
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            CancellationToken cancellationToken)
        {
            var reporte = await _ventaService.ObtenerReporteVentasAsync(fechaInicio, fechaFin, cancellationToken);
            return Ok(reporte);
        }
    }
}
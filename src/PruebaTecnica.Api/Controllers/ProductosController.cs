using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Api.Dtos.Productos.Requests;
using PruebaTecnica.Api.Dtos.Productos.Responses;
using PruebaTecnica.Api.Services.Interfaces;

namespace PruebaTecnica.Api.Controllers
{
    [ApiController]
    [Route("api/productos")]
    [Produces("application/json")]
    [Tags("2. Administración de Productos")]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        /// <summary>Obtiene el listado de productos, opcionalmente filtrados por código.</summary>
        /// <param name="codigo">Código opcional para filtrar los productos.</param>
        /// <param name="cancellationToken">Token de cancelación de la solicitud.</param>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductos(
            [FromQuery] string? codigo,
            CancellationToken cancellationToken)
        {
            var productos = await _productoService.ObtenerProductosAsync(codigo, cancellationToken);
            return Ok(productos);
        }

        /// <summary>Registra un nuevo producto en el sistema.</summary>
        /// <param name="request">Información requerida para el registro del producto.</param>
        /// <param name="cancellationToken">Token de cancelación de la solicitud.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CrearProducto(
            [FromBody] ProductoCreateRequest request,
            CancellationToken cancellationToken)
        {
            await _productoService.CrearProductoAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, new { success = true, message = "Producto creado con éxito." });
        }

        /// <summary>Actualiza la información de un producto existente.</summary>
        /// <param name="id">Identificador único del producto.</param>
        /// <param name="request">Datos actualizados del producto.</param>
        /// <param name="cancellationToken">Token de cancelación de la solicitud.</param>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ActualizarProducto(
            int id,
            [FromBody] ProductoUpdateRequest request,
            CancellationToken cancellationToken)
        {
            await _productoService.ActualizarProductoAsync(id, request, cancellationToken);
            return Ok(new { success = true, message = "Producto actualizado correctamente." });
        }

        /// <summary>Elimina un producto del sistema por su identificador.</summary>
        /// <param name="id">Identificador único del producto.</param>
        /// <param name="cancellationToken">Token de cancelación de la solicitud.</param>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EliminarProducto(
            int id,
            CancellationToken cancellationToken)
        {
            await _productoService.EliminarProductoAsync(id, cancellationToken);
            return Ok(new { success = true, message = "Producto eliminado correctamente." });
        }
    }
}
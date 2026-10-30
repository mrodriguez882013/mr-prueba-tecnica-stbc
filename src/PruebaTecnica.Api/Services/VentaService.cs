using PruebaTecnica.Api.Dtos.Ventas.Requests;
using PruebaTecnica.Api.Dtos.Ventas.Responses;
using PruebaTecnica.Api.Exceptions;
using PruebaTecnica.Api.Repositories.Interfaces;
using PruebaTecnica.Api.Services.Interfaces;
using System.Data;

namespace PruebaTecnica.Api.Services;

public class VentaService : IVentaService
{
    private readonly IVentaRepository _ventaRepository;
    private readonly IProductoRepository _productoRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public VentaService(
        IVentaRepository ventaRepository,
        IProductoRepository productoRepository,
        IUsuarioRepository usuarioRepository)
    {
        _ventaRepository = ventaRepository;
        _productoRepository = productoRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<VentaResponse> RegistrarVentaAsync(
        VentaCreateRequest request, 
        CancellationToken cancellationToken = default)
    {
        // Validar existencia de usuario
        var usuario = await _usuarioRepository.ObtenerPorIdAsync(request.IdUsuarioCreacion, cancellationToken)
            ?? throw new NotFoundException("El usuario especificado para registrar la venta no existe.");

        if (request.Detalles == null || request.Detalles.Count == 0)
        {
            throw new ValidationException("La venta debe contener al menos un detalle de producto.");
        }

        // Validar stock de cada producto en la venta
        var todosProductos = await _productoRepository.ObtenerProductosAsync(null, cancellationToken);

        var detalleTable = new DataTable();
        detalleTable.Columns.Add("IdPro", typeof(int));
        detalleTable.Columns.Add("Cantidad", typeof(int));
        detalleTable.Columns.Add("Precio", typeof(decimal));
        detalleTable.Columns.Add("Iva", typeof(decimal));
        detalleTable.Columns.Add("Total", typeof(decimal));

        decimal totalCalculado = 0;

        foreach (var item in request.Detalles)
        {
            var producto = todosProductos.FirstOrDefault(p => p.IdPro == item.IdPro)
                ?? throw new NotFoundException($"El producto con ID {item.IdPro} no existe.");

            if (producto.Stock < item.Cantidad)
            {
                throw new ValidationException($"Stock insuficiente para el producto '{producto.ProductoNombre}'. Disponible: {producto.Stock}, Solicitado: {item.Cantidad}.");
            }

            totalCalculado += item.Total;

            detalleTable.Rows.Add(
                item.IdPro,
                item.Cantidad,
                item.Precio,
                item.Iva,
                item.Total);
        }

        int idVentaGenerado = await _ventaRepository.RegistrarVentaAsync(request.Vendedor, totalCalculado, request.IdUsuarioCreacion, detalleTable, cancellationToken);

        return new VentaResponse
        {
            IdVenta = idVentaGenerado,
            Fecha = DateTime.Now,
            Vendedor = request.Vendedor,
            Total = totalCalculado,
            IdUsuarioCreacion = request.IdUsuarioCreacion
        };
    }

    public async Task<IEnumerable<VentaReporteResponse>> ObtenerReporteVentasAsync(
        DateTime? fechaInicio,
        DateTime? fechaFin,
        CancellationToken cancellationToken = default)
    {
        if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
        {
            throw new ValidationException("La fecha de inicio no puede ser posterior a la fecha de fin.");
        }

        return await _ventaRepository.ObtenerReporteVentasAsync(fechaInicio, fechaFin, cancellationToken);
    }
}
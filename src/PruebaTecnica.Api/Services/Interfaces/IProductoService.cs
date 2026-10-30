using PruebaTecnica.Api.Dtos.Productos.Requests;
using PruebaTecnica.Api.Dtos.Productos.Responses;

namespace PruebaTecnica.Api.Services.Interfaces;

public interface IProductoService
{
    Task<IEnumerable<ProductoResponse>> ObtenerProductosAsync(
        string? codigo,
        CancellationToken cancellationToken = default);

    Task CrearProductoAsync(
        ProductoCreateRequest request,
        CancellationToken cancellationToken = default);

    Task ActualizarProductoAsync(
        int id, 
        ProductoUpdateRequest request,
        CancellationToken cancellationToken = default);

    Task EliminarProductoAsync(
        int id, 
        CancellationToken cancellationToken = default);
}
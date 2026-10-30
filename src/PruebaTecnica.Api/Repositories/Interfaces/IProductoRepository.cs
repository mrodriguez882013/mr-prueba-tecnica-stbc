using PruebaTecnica.Api.Entities;

namespace PruebaTecnica.Api.Repositories.Interfaces;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> ObtenerProductosAsync(
        string? codigo,
        CancellationToken cancellationToken = default);
    Task InsertarProductoAsync(
        Producto producto,
        CancellationToken cancellationToken = default);
    Task ActualizarProductoAsync(
        int id,
        Producto producto,
        CancellationToken cancellationToken = default);

    Task EliminarProductoAsync(
        int id,
        CancellationToken cancellationToken = default
        );
}
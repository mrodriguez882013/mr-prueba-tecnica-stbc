using PruebaTecnica.Api.Entities;

namespace PruebaTecnica.Api.Repositories.Interfaces;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> ObtenerProductosAsync(string? codigo);
    Task InsertarProductoAsync(Producto producto);
    Task ActualizarProductoAsync(int id, Producto producto);
    Task EliminarProductoAsync(int id);
}
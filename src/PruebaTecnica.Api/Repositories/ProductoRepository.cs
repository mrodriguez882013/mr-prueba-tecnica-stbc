using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Api.Data;
using PruebaTecnica.Api.Entities;
using PruebaTecnica.Api.Repositories.Interfaces;

namespace PruebaTecnica.Api.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly PruebaTecnicaDbContext _context;

    public ProductoRepository(PruebaTecnicaDbContext context)
    {
        _context = context;
    }

    //------------------
    // Lecturas
    //------------------

    public async Task<IEnumerable<Producto>> ObtenerProductosAsync(string? codigo)
    {
        var pCodigo = new SqlParameter("@codigo", (object?)codigo ?? DBNull.Value);
        return await _context.Productos
            .FromSqlRaw("EXEC dbo.UspObtenerProductos @codigo", pCodigo)
            .ToListAsync();
    }

    //-------------------
    // Escrituras
    //-------------------

    public async Task InsertarProductoAsync(Producto producto)
    {
        var pCodigo = new SqlParameter("@codigo", producto.Codigo);
        var pProducto = new SqlParameter("@producto", producto.ProductoNombre);
        var pPrecio = new SqlParameter("@precio", producto.Precio);
        var pStock = new SqlParameter("@stock", producto.Stock);
        var pUser = new SqlParameter("@idUsuarioCreacion", producto.IdUsuarioCreacion);

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC dbo.UspInsertarProducto @codigo, @producto, @precio, @stock, @idUsuarioCreacion",
            pCodigo, pProducto, pPrecio, pStock, pUser);
    }

    public async Task ActualizarProductoAsync(int id, Producto producto)
    {
        var pId = new SqlParameter("@idpro", id);
        var pCodigo = new SqlParameter("@codigo", producto.Codigo);
        var pProducto = new SqlParameter("@producto", producto.ProductoNombre);
        var pPrecio = new SqlParameter("@precio", producto.Precio);
        var pStock = new SqlParameter("@stock", producto.Stock);

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC dbo.UspActualizarProducto @idpro, @codigo, @producto, @precio, @stock",
            pId, pCodigo, pProducto, pPrecio, pStock);
    }

    public async Task EliminarProductoAsync(int id)
    {
        var pId = new SqlParameter("@idpro", id);
        await _context.Database.ExecuteSqlRawAsync("EXEC dbo.UspEliminarProducto @idpro", pId);
    }
}
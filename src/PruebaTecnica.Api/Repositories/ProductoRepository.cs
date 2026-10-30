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

    public async Task<IEnumerable<Producto>> ObtenerProductosAsync(
        string? codigo,
        CancellationToken cancellationToken = default)
    {
        var pCodigo = new SqlParameter("@codigo", (object?)codigo ?? DBNull.Value);
        return await _context.Productos
            .FromSqlRaw("EXEC dbo.UspObtenerProductos @codigo", pCodigo)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    //-------------------
    // Escrituras
    //-------------------

    public async Task InsertarProductoAsync(
      Producto producto,
      CancellationToken cancellationToken = default)
    {
        var pCodigo = new SqlParameter("@codigo", producto.Codigo);
        var pProducto = new SqlParameter("@producto", producto.ProductoNombre);
        var pPrecio = new SqlParameter("@precio", producto.Precio);
        var pStock = new SqlParameter("@stock", producto.Stock);
        var pUser = new SqlParameter("@idUsuarioCreacion", producto.IdUsuarioCreacion);

        await _context.Database.ExecuteSqlRawAsync(
            sql: "EXEC dbo.UspInsertarProducto @codigo, @producto, @precio, @stock, @idUsuarioCreacion",
            parameters: [pCodigo, pProducto, pPrecio, pStock, pUser],
            cancellationToken: cancellationToken
        );
    }

    public async Task ActualizarProductoAsync(
       int id,
       Producto producto,
       CancellationToken cancellationToken = default)
    {
        var pId = new SqlParameter("@idpro", id);
        var pCodigo = new SqlParameter("@codigo", producto.Codigo);
        var pProducto = new SqlParameter("@producto", producto.ProductoNombre);
        var pPrecio = new SqlParameter("@precio", producto.Precio);
        var pStock = new SqlParameter("@stock", producto.Stock);

        await _context.Database.ExecuteSqlRawAsync(
            sql: "EXEC dbo.UspActualizarProducto @idpro, @codigo, @producto, @precio, @stock",
            parameters: [pId, pCodigo, pProducto, pPrecio, pStock],
            cancellationToken: cancellationToken
        );
    }

    public async Task EliminarProductoAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var pId = new SqlParameter("@idpro", id);

        await _context.Database.ExecuteSqlRawAsync(
            sql: "EXEC dbo.UspEliminarProducto @idpro",
            parameters: [pId],
            cancellationToken: cancellationToken
        );
    }

}
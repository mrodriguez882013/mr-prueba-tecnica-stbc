using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Api.Data;
using PruebaTecnica.Api.Dtos.Ventas.Responses;
using PruebaTecnica.Api.Repositories.Interfaces;
using System.Data;

namespace PruebaTecnica.Api.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly PruebaTecnicaDbContext _context;

        public VentaRepository(PruebaTecnicaDbContext context)
        {
            _context = context;
        }

        public async Task<int> RegistrarVentaAsync(string vendedor, decimal total, int idUsuarioCreacion, DataTable detalleTable)
        {
            var pVendedor = new SqlParameter("@vendedor", vendedor);
            var pTotal = new SqlParameter("@total", total);
            var pUsuarioCreacion = new SqlParameter("@idUsuarioCreacion", idUsuarioCreacion);

            var pDetalle = new SqlParameter("@detalle", detalleTable)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = "dbo.TipoDetalleVenta"
            };

            var pIdVenta = new SqlParameter("@idventa", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC dbo.UspRegistrarVenta @vendedor, @total, @idUsuarioCreacion, @detalle, @idventa OUTPUT",
                pVendedor, pTotal, pUsuarioCreacion, pDetalle, pIdVenta);

            return Convert.ToInt32(pIdVenta.Value);
        }

        public async Task<IEnumerable<VentaReporteResponse>> ObtenerReporteVentasAsync(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var pFechaInicio = new SqlParameter("@fechaInicio", (object?)fechaInicio ?? DBNull.Value);
            var pFechaFin = new SqlParameter("@fechaFin", (object?)fechaFin ?? DBNull.Value);

            return await _context.Database
                .SqlQueryRaw<VentaReporteResponse>(
                    "EXEC dbo.UspReporteVentas @fechaInicio, @fechaFin",
                    pFechaInicio, pFechaFin)
                .ToListAsync();
        }
    }
}
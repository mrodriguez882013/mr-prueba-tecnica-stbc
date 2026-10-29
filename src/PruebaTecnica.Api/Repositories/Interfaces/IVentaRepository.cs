using PruebaTecnica.Api.Dtos.Ventas.Responses;
using System.Data;

namespace PruebaTecnica.Api.Repositories.Interfaces
{
    public interface IVentaRepository
    {
        Task<int> RegistrarVentaAsync(string vendedor, decimal total, int idUsuarioCreacion, DataTable detalleTable);
        Task<IEnumerable<VentaReporteResponse>> ObtenerReporteVentasAsync(DateTime? fechaInicio, DateTime? fechaFin);
    }
}
using PruebaTecnica.Api.Dtos.Ventas.Requests;
using PruebaTecnica.Api.Dtos.Ventas.Responses;

namespace PruebaTecnica.Api.Services.Interfaces;

public interface IVentaService
{
    Task<VentaResponse> RegistrarVentaAsync(
        VentaCreateRequest request, 
        CancellationToken cancellationToken = default);
    Task<IEnumerable<VentaReporteResponse>> ObtenerReporteVentasAsync(
        DateTime? fechaInicio, 
        DateTime? fechaFin, 
        CancellationToken cancellationToken = default);
}
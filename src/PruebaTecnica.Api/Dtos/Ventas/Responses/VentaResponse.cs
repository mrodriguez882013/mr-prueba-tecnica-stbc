namespace PruebaTecnica.Api.Dtos.Ventas.Responses;

public class VentaResponse
{
    public int IdVenta { get; set; }
    public DateTime Fecha { get; set; }
    public string Vendedor { get; set; } = null!;
    public decimal Total { get; set; }
    public int IdUsuarioCreacion { get; set; }
}
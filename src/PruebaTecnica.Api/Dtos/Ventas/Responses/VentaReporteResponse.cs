namespace PruebaTecnica.Api.Dtos.Ventas.Responses;

public class VentaReporteResponse
{
    public int IdVenta { get; set; }
    public DateTime Fecha { get; set; }
    public string Vendedor { get; set; } = null!;
    public decimal TotalVenta { get; set; }
    public string UsuarioRegistra { get; set; } = null!;
    public string CodigoProducto { get; set; } = null!;
    public string NombreProducto { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Iva { get; set; }
    public decimal TotalDetalle { get; set; }
}
namespace PruebaTecnica.Api.Dtos.Productos.Responses;

public class ProductoResponse
{
    public int IdPro { get; set; }
    public string Codigo { get; set; } = null!;
    public string Producto { get; set; } = null!;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public int IdUsuarioCreacion { get; set; }
    public DateTime FechaCreacion { get; set; }
}
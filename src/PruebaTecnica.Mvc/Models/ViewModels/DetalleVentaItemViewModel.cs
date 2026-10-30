namespace PruebaTecnica.Mvc.Models.ViewModels;

public class DetalleVentaItemViewModel
{
    public int IdPro { get; set; }
    public string Codigo { get; set; } = null!;
    public string ProductoNombre { get; set; } = null!;
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public decimal Iva { get; set; }
    public decimal Total { get; set; }
}

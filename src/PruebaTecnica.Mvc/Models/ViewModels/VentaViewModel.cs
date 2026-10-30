namespace PruebaTecnica.Mvc.Models.ViewModels;

public class VentaViewModel
{
    public string Vendedor { get; set; } = null!;
    public int IdUsuarioCreacion { get; set; }
    public List<DetalleVentaItemViewModel> Detalles { get; set; } = [];
}

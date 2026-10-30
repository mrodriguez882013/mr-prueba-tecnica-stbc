using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Mvc.Models.Dtos.Ventas.Requests;

public class DetalleVentaCreateRequest
{
    [Required(ErrorMessage = "El ID del producto es obligatorio.")]
    public int IdPro { get; set; }

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "El precio unitario es obligatorio.")]
    [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El IVA es obligatorio.")]
    public decimal Iva { get; set; }

    [Required(ErrorMessage = "El total del detalle es obligatorio.")]
    public decimal Total { get; set; }
}

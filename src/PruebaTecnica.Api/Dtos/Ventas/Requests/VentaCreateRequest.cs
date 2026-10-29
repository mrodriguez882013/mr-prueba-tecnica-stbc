using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Api.Dtos.Ventas.Requests;

public class VentaCreateRequest
{
    [Required(ErrorMessage = "El nombre del vendedor es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre del vendedor no puede superar los 100 caracteres.")]
    public string Vendedor { get; set; } = null!;

    [Required(ErrorMessage = "El ID del usuario creador es obligatorio.")]
    public int IdUsuarioCreacion { get; set; }

    [Required(ErrorMessage = "La venta debe contener al menos un detalle.")]
    public List<DetalleVentaCreateRequest> Detalles { get; set; } = new();
}
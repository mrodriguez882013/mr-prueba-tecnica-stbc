using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Api.Dtos.Productos.Requests;

public class ProductoCreateRequest
{
    [Required(ErrorMessage = "El código del producto es obligatorio.")]
    [StringLength(50, ErrorMessage = "El código no puede superar los 50 caracteres.")]
    public string Codigo { get; set; } = null!;

    [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
    [StringLength(150, ErrorMessage = "El nombre del producto no puede superar los 150 caracteres.")]
    public string Producto { get; set; } = null!;

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio.")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "El ID del usuario creador es obligatorio.")]
    public int IdUsuarioCreacion { get; set; }
}
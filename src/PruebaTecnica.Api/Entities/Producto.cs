using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Api.Entities;

[Table("Productos")]
public class Producto
{
    [Key]
    public int IdPro { get; set; }

    [Required]
    [MaxLength(50)]
    public string Codigo { get; set; } = null!;

    [Required]
    [Column("Producto")]
    [MaxLength(150)]
    public string ProductoNombre { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    [Required]
    public int Stock { get; set; }

    // Clave foránea del usuario que creó el producto
    [Required]
    public int IdUsuarioCreacion { get; set; }

    [Required]
    public DateTime FechaCreacion { get; set; }

    //---------------------------
    // Propiedades de navegación
    //---------------------------

    // Relación N:1 con Usuario (El creador del producto)
    [ForeignKey("IdUsuarioCreacion")]
    public virtual Usuario UsuarioCreacion { get; set; } = null!;

    // Relación 1:N con DetalleVentas (Un producto aparece en múltiples detalles de venta)
    public virtual ICollection<DetalleVenta> DetalleVentas { get; set; } = [];
}
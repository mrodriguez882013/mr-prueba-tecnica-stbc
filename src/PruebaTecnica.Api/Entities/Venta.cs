using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Api.Entities;

[Table("Ventas")]
public class Venta
{
    [Key]
    public int IdVenta { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Required]
    [MaxLength(100)]
    public string Vendedor { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    // Clave foránea del usuario que registró la venta
    [Required]
    public int IdUsuarioCreacion { get; set; }


    //---------------------------
    // Propiedades de navegación
    //---------------------------

    // Relación N:1 con Usuario (Operador que registró la venta)
    [ForeignKey("IdUsuarioCreacion")]
    public virtual Usuario UsuarioCreacion { get; set; } = null!;

    // Relación 1:N con DetalleVentas (Los ítems comprados en esta venta)
    public virtual ICollection<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();
}
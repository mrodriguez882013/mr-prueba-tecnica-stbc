using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Api.Entities;

[Table("DetalleVentas")]
public class DetalleVenta
{
    [Key]
    public int IdDe { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    // Claves foráneas
    [Required]
    public int IdVenta { get; set; }

    [Required]
    public int IdPro { get; set; }

    [Required]
    public int Cantidad { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Iva { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    //---------------------------
    // Propiedades de navegación
    //---------------------------

    // Relación N:1 con Venta (Pertenece a una cabecera de venta)
    [ForeignKey("IdVenta")]
    public virtual Venta Venta { get; set; } = null!;

    // Relación N:1 con Producto (Hace referencia al producto vendido)
    [ForeignKey("IdPro")]
    public virtual Producto Producto { get; set; } = null!;
}
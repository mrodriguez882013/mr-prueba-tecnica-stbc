using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Api.Entities;

[Table("Usuarios")]
public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Rol { get; set; } = null!; // 'Administrador', 'Operador'

    //---------------------------
    // Propiedades de navegación
    //---------------------------

    // Un usuario puede haber registrado múltiples productos
    public virtual ICollection<Producto> ProductosCreados { get; set; } = [];

    // Un usuario puede haber registrado múltiples ventas
    public virtual ICollection<Venta> VentasCreadas { get; set; } = [];
}
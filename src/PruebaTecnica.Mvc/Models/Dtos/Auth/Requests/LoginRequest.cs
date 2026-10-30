using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Mvc.Models.Dtos.Auth.Requests;

public class LoginRequest
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [StringLength(50, ErrorMessage = "El usuario no puede superar los 50 caracteres.")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(255, ErrorMessage = "La contraseña excede el límite permitido.")]
    public string Password { get; set; } = null!;
}
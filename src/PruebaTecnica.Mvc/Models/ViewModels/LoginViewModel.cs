using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Mvc.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "El usuario es obligatorio.")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Password { get; set; } = null!;
}
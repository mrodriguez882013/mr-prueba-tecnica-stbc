namespace PruebaTecnica.Api.Dtos.Login.Responses;

public class LoginResponse
{
    public int IdUsuario { get; set; }
    public string Username { get; set; } = null!;
    public string Rol { get; set; } = null!;
    public string TokenOrMessage { get; set; } = null!;
}

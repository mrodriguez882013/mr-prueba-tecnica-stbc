using PruebaTecnica.Api.Dtos.Login.Requests;
using PruebaTecnica.Api.Dtos.Login.Responses;
using PruebaTecnica.Api.Exceptions;
using PruebaTecnica.Api.Repositories.Interfaces;
using PruebaTecnica.Api.Services.Interfaces;

namespace PruebaTecnica.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<LoginResponse> LoginAsync(
        LoginRequest request, 
        CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioRepository.ObtenerPorUsernameAsync(request.Username, cancellationToken) 
            ?? throw new ValidationException("Credenciales inválidas. Verifique el usuario o contraseña.");

        bool passwordValida = BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash);

        if (!passwordValida)
        {
            throw new ValidationException("Credenciales inválidas. Verifique el usuario o contraseña.");
        }

        return new LoginResponse
        {
            IdUsuario = usuario.IdUsuario,
            Username = usuario.Username,
            Rol = usuario.Rol,
            TokenOrMessage = "Autenticación exitosa"
        };
    }
}
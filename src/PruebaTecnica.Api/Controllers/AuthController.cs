using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Api.Dtos.Login.Requests;
using PruebaTecnica.Api.Dtos.Login.Responses;
using PruebaTecnica.Api.Services.Interfaces;

namespace PruebaTecnica.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Produces("application/json")]
    [Tags("1. Autenticación y Seguridad")]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>Autentica a un usuario en el sistema validando sus credenciales (BCrypt).</summary>
        /// <param name="request">Credenciales de acceso (Username y Password).</param>
        /// <param name="cancellationToken">Token de cancelación de la solicitud.</param>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _authService.LoginAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
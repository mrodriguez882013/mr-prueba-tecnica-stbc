using PruebaTecnica.Api.Dtos.Login.Requests;
using PruebaTecnica.Api.Dtos.Login.Responses;

namespace PruebaTecnica.Api.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(
        LoginRequest request, 
        CancellationToken cancellationToken = default);
}
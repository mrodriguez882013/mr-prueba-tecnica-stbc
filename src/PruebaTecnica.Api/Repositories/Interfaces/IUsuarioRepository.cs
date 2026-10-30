using PruebaTecnica.Api.Entities;

namespace PruebaTecnica.Api.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorUsernameAsync(
            string username, 
            CancellationToken cancellationToken = default);
        Task<Usuario?> ObtenerPorIdAsync(
            int idUsuario,
            CancellationToken cancellationToken = default);
    }
}
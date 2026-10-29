using PruebaTecnica.Api.Entities;

namespace PruebaTecnica.Api.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorUsernameAsync(string username);
        Task<Usuario?> ObtenerPorIdAsync(int idUsuario);
    }
}
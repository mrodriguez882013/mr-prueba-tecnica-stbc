using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Api.Data;
using PruebaTecnica.Api.Entities;
using PruebaTecnica.Api.Repositories.Interfaces;

namespace PruebaTecnica.Api.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly PruebaTecnicaDbContext _context;

    public UsuarioRepository(PruebaTecnicaDbContext context)
    {
        _context = context;
    }

    //---------------------------
    // Lecturas
    //---------------------------

    public async Task<Usuario?> ObtenerPorUsernameAsync(
        string username, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken: cancellationToken);
    }

    public async Task<Usuario?> ObtenerPorIdAsync(
        int idUsuario,
        CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios
            .FindAsync([idUsuario], cancellationToken: cancellationToken);
    }
}
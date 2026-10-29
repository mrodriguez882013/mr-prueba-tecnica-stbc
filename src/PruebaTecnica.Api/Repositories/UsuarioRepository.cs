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

    public async Task<Usuario?> ObtenerPorUsernameAsync(string username)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int idUsuario)
    {
        return await _context.Usuarios
            .FindAsync(idUsuario);
    }
}
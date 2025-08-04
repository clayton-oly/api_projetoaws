using Microsoft.EntityFrameworkCore;
using SocialApp.Data;
using SocialApp.Interfaces;
using SocialApp.model;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly SocialAppDbContext _context;

    public UsuarioRepository(SocialAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> GetUsuarioByIdAsync(int id)
    {
      return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.ID == id);
    }

    public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task UpdateUsuarioAsync(Usuario usuario)
    {
        _context.Entry(usuario).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteUsuarioAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return false;

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }
}


using Microsoft.EntityFrameworkCore;
using SocialApp.Data;
using SocialApp.model;

public class UsuarioRepository
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
        return await _context.Usuarios.FindAsync(id);
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

    public async Task DeleteUsuarioAsync(int id)
    {
        var usuarioToDelete = await _context.Usuarios.FindAsync(id);
        if (usuarioToDelete == null)
        {
            throw new UsuarioNotFoundException();
        }

        _context.Usuarios.Remove(usuarioToDelete);
        await _context.SaveChangesAsync();
    }
}

public class UsuarioNotFoundException : Exception
{
    public UsuarioNotFoundException() : base("Usuário não encontrado.")
    {
    }
}

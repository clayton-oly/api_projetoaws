using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrioConnect.Data;
using TrioConnect.model;

public class UsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
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

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrioConnect;
using TrioConnect.Data;
using TrioConnect.model;

public class PostagemRepository
{
    private readonly ApplicationDbContext _context;

    public PostagemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Postagem>> GetAllPostagensAsync()
    {
        return await _context.Postagens.ToListAsync();
    }

    public async Task<Postagem> GetPostagemByIdAsync(int id)
    {
        return await _context.Postagens.FindAsync(id);
    }

    public async Task<Postagem> CreatePostagemAsync(Postagem postagem)
    {
        _context.Postagens.Add(postagem);
        await _context.SaveChangesAsync();
        return postagem;
    }

    public async Task UpdatePostagemAsync(Postagem postagem)
    {
        _context.Entry(postagem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeletePostagemAsync(int id)
    {
        var postagemToDelete = await _context.Postagens.FindAsync(id);
        if (postagemToDelete == null)
        {
            throw new PostagemNotFoundException();
        }

        _context.Postagens.Remove(postagemToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<Usuario> GetUsuarioByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Tema> GetTemaByIdAsync(int id)
    {
        return await _context.Temas.FindAsync(id);
    }
}

public class PostagemNotFoundException : Exception
{
    public PostagemNotFoundException() : base("Postagem não encontrada.")
    {
    }
}

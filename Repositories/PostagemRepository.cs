using Microsoft.EntityFrameworkCore;
using SocialApp;
using SocialApp.Data;
using SocialApp.Interfaces;
using SocialApp.model;

public class PostagemRepository : IPostagemRepository
{
    private readonly SocialAppDbContext _context;

    public PostagemRepository(SocialAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Postagem>> GetAllPostagensAsync()
    {
        return await _context.Postagens.ToListAsync();
    }

    public async Task<Postagem> GetPostagemByIdAsync(int id)
    {
        return await _context.Postagens.AsNoTracking().FirstOrDefaultAsync(p => p.ID == id);
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

    public async Task<bool> DeletePostagemAsync(int id)
    {
        var postagem = await _context.Postagens.FindAsync(id);
        if (postagem == null) return false;

        _context.Postagens.Remove(postagem);
        await _context.SaveChangesAsync();
        return true;
    }
}

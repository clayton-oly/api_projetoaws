using Microsoft.EntityFrameworkCore;
using SocialApp.Data;
using SocialApp.Interfaces;
using SocialApp.model;

public class TemaRepository : ITemaRepository
{
    private readonly SocialAppDbContext _context;

    public TemaRepository(SocialAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tema>> GetAllTemasAsync()
    {
        return await _context.Temas.ToListAsync();
    }

    public async Task<Tema> GetTemaByIdAsync(int id)
    {
        return await _context.Temas.AsNoTracking().FirstOrDefaultAsync(t => t.ID == id);
    }

    public async Task<Tema> CreateTemaAsync(Tema tema)
    {
        _context.Temas.Add(tema);
        await _context.SaveChangesAsync();
        return tema;
    }

    public async Task UpdateTemaAsync(Tema tema)
    {
        _context.Entry(tema).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteTemaAsync(int id)
    {
        var tema = await _context.Temas.FindAsync(id);
        if (tema == null) return false;

        _context.Temas.Remove(tema);
        await _context.SaveChangesAsync();
        return true;
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrioConnect.Data;
using TrioConnect.model;

public class TemaRepository
{
    private readonly ApplicationDbContext _context;

    public TemaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tema>> GetAllTemasAsync()
    {
        return await _context.Temas.ToListAsync();
    }

    public async Task<Tema> GetTemaByIdAsync(int id)
    {
        return await _context.Temas.FindAsync(id);
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

    public async Task DeleteTemaAsync(int id)
    {
        var temaToDelete = await _context.Temas.FindAsync(id);
        if (temaToDelete == null)
        {
            throw new TemaNotFoundException();
        }

        _context.Temas.Remove(temaToDelete);
        await _context.SaveChangesAsync();
    }
}

public class TemaNotFoundException : Exception
{
    public TemaNotFoundException() : base("Tema não encontrado.")
    {
    }
}

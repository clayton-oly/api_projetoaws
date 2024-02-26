using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrioConnect.Data;
using TrioConnect.model;

[Route("api/[controller]")]
[ApiController]
public class TemaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TemaController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tema>>> GetTemas()
    {
        return await _context.Temas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tema>> GetTema(int id)
    {
        var tema = await _context.Temas.FindAsync(id);

        if (tema == null)
        {
            return NotFound();
        }

        return tema;
    }


    [HttpPost]
    public async Task<ActionResult<Tema>> PostTema(Tema tema)
    {
        // Verificar se a descrição é nula ou vazia
        if (string.IsNullOrWhiteSpace(tema.Descricao))
        {
            return BadRequest("O campo Descrição é obrigatório.");
        }

        // Verificar se a descrição tem pelo menos 3 caracteres
        if (tema.Descricao.Length < 3)
        {
            return BadRequest("O campo Descrição deve conter pelo menos 3 caracteres.");
        }

        // Se todas as validações passarem, adicionar o novo tema ao contexto
        _context.Temas.Add(tema);

        // Salvar as mudanças no banco de dados
        await _context.SaveChangesAsync();

        // Retornar o novo tema criado
        return CreatedAtAction(nameof(GetTema), new { id = tema.ID }, tema);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTema(int id, Tema tema)
    {
        if (id != tema.ID)
        {
            return BadRequest();
        }

        // Verificar se a descrição é nula ou vazia
        if (string.IsNullOrWhiteSpace(tema.Descricao))
        {
            return BadRequest("O campo Descrição é obrigatório.");
        }

        // Verificar se a descrição tem pelo menos 3 caracteres
        if (tema.Descricao.Length < 3)
        {
            return BadRequest("O campo Descrição deve conter pelo menos 3 caracteres.");
        }

        _context.Entry(tema).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TemaExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTema(int id)
    {
        var tema = await _context.Temas.FindAsync(id);
        if (tema == null)
        {
            return NotFound();
        }

        _context.Temas.Remove(tema);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TemaExists(int id)
    {
        return _context.Temas.Any(e => e.ID == id);
    }
}

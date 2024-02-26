using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrioConnect;
using TrioConnect.Data;

[Route("api/[controller]")]
[ApiController]
public class PostagemController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PostagemController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Postagem>>> GetPostagens()
    {
        return await _context.Postagens.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Postagem>> GetPostagem(int id)
    {
        var postagem = await _context.Postagens.FindAsync(id);

        if (postagem == null)
        {
            return NotFound();
        }

        return postagem;
    }

    [HttpPost]
    public async Task<ActionResult<Postagem>> CriarPostagem([FromBody] Postagem postagem)
    {
        try
        {
            // Verifica se a postagem recebida é nula
            if (postagem == null)
            {
                return BadRequest("Dados da postagem não fornecidos.");
            }

            // Verifica se os IDs de usuário e tema foram fornecidos
            if (postagem.UsuarioID == 0 || postagem.TemaID == 0)
            {
                return BadRequest("IDs de usuário e tema são obrigatórios.");
            }

            // Verifica se o usuário e o tema existem no banco de dados
            var usuario = await _context.Usuarios.FindAsync(postagem.UsuarioID);
            var tema = await _context.Temas.FindAsync(postagem.TemaID);
            if (usuario == null || tema == null)
            {
                return BadRequest("Usuário ou tema não encontrado.");
            }

            // Validação do título e texto
            if (string.IsNullOrEmpty(postagem.Titulo) || postagem.Titulo.Length < 3)
            {
                return BadRequest("O título é obrigatório e deve ter no mínimo 3 caracteres.");
            }

            if (string.IsNullOrEmpty(postagem.Texto) || postagem.Texto.Length < 10)
            {
                return BadRequest("O texto é obrigatório e deve ter no mínimo 10 caracteres.");
            }

            // Grava a data de entrada no banco de dados
            postagem.Data = DateTime.UtcNow;

            // Associa o usuário e o tema à postagem
            postagem.Usuario = usuario;
            postagem.Tema = tema;

            // Adiciona a nova postagem ao contexto e salva no banco de dados
            _context.Postagens.Add(postagem);
            await _context.SaveChangesAsync();

            // Retorna a nova postagem criada
            return CreatedAtAction(nameof(GetPostagem), new { id = postagem.ID }, postagem);
        }
        catch (Exception ex)
        {
            // Se ocorrer algum erro inesperado, retorna um status de erro interno do servidor
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutPostagem(int id, Postagem postagem)
    {
        if (id != postagem.ID)
        {
            return BadRequest();
        }

        // Verifica se a postagem recebida é nula
        if (postagem == null)
        {
            return BadRequest("Dados da postagem não fornecidos.");
        }

        // Verifica se os IDs de usuário e tema foram fornecidos
        if (postagem.UsuarioID == 0 || postagem.TemaID == 0)
        {
            return BadRequest("IDs de usuário e tema são obrigatórios.");
        }

        // Verifica se o usuário e o tema existem no banco de dados
        var usuario = await _context.Usuarios.FindAsync(postagem.UsuarioID);
        var tema = await _context.Temas.FindAsync(postagem.TemaID);
        if (usuario == null || tema == null)
        {
            return BadRequest("Usuário ou tema não encontrado.");
        }

        // Validação do título e texto
        if (string.IsNullOrEmpty(postagem.Titulo) || postagem.Titulo.Length < 3)
        {
            return BadRequest("O título é obrigatório e deve ter no mínimo 3 caracteres.");
        }

        if (string.IsNullOrEmpty(postagem.Texto) || postagem.Texto.Length < 10)
        {
            return BadRequest("O texto é obrigatório e deve ter no mínimo 10 caracteres.");
        }

        // Grava a data de entrada no banco de dados
        postagem.Data = DateTime.UtcNow;

        _context.Entry(postagem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PostagemExists(id))
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
    public async Task<IActionResult> DeletePostagem(int id)
    {
        var postagem = await _context.Postagens.FindAsync(id);
        if (postagem == null)
        {
            return NotFound();
        }

        _context.Postagens.Remove(postagem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PostagemExists(int id)
    {
        return _context.Postagens.Any(e => e.ID == id);
    }
}

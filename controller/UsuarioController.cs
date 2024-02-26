using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrioConnect.Data;
using TrioConnect.model;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsuarioController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        return await _context.Usuarios.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            return NotFound();
        }

        return usuario;
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        // Verificar se o email é nulo ou vazio
        if (string.IsNullOrWhiteSpace(usuario.Email))
        {
            return BadRequest("O campo Email é obrigatório.");
        }

        // Verificar se o email está em um formato válido
        if (!IsValidEmail(usuario.Email))
        {
            return BadRequest("O formato do Email é inválido.");
        }

        // Verificar se já existe um usuário com o mesmo e-mail
        var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);

        if (existingUser != null)
        {
            // Se já existir um usuário com o mesmo e-mail, retornar um erro
            return BadRequest("Já existe um usuário cadastrado com este e-mail.");
        }

        // Verificar se o nome tem pelo menos 3 caracteres
        if (string.IsNullOrWhiteSpace(usuario.Nome) || usuario.Nome.Length < 3)
        {
            return BadRequest("O campo Nome deve conter pelo menos 3 caracteres.");
        }

        // Se todas as validações passarem, adicionar o novo usuário ao contexto
        _context.Usuarios.Add(usuario);

        // Salvar as mudanças no banco de dados
        await _context.SaveChangesAsync();

        // Retornar o novo usuário criado
        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.ID }, usuario);
    }

    // Função auxiliar para validar o formato do email
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
    {
        if (id != usuario.ID)
        {
            return BadRequest();
        }

        // Verificar se o email é nulo ou vazio
        if (string.IsNullOrWhiteSpace(usuario.Email))
        {
            return BadRequest("O campo Email é obrigatório.");
        }

        // Verificar se o email está em um formato válido
        if (!IsValidEmail(usuario.Email))
        {
            return BadRequest("O formato do Email é inválido.");
        }

        // Verificar se o nome tem pelo menos 3 caracteres
        if (string.IsNullOrWhiteSpace(usuario.Nome) || usuario.Nome.Length < 3)
        {
            return BadRequest("O campo Nome deve conter pelo menos 3 caracteres.");
        }

        _context.Entry(usuario).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UsuarioExists(id))
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
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UsuarioExists(int id)
    {
        return _context.Usuarios.Any(e => e.ID == id);
    }
}

using Microsoft.AspNetCore.Mvc;
using SocialApp.Interfaces;
using SocialApp.ViewModels;

namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioOutputViewModel>>> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();

            if (usuarios == null || !usuarios.Any())
                return NoContent();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioOutputViewModel>> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);

            if (usuario == null)
                return NotFound("Usu�rio n�o encontrado.");

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioInputViewModel>> PostUsuario(UsuarioInputViewModel usuarioViewModel)
        {
            var createdUsuario = await _usuarioService.CreateUsuarioAsync(usuarioViewModel);

            if (createdUsuario == null)
                return BadRequest("Erro ao criar usu�rio.");

            return CreatedAtAction(nameof(GetUsuarioById), new { id = createdUsuario.Id }, createdUsuario);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioInputViewModel usuarioViewModel)
        {

            var usuarioExistente = await _usuarioService.GetUsuarioByIdAsync(id);

            if (usuarioExistente == null)
                return NotFound("Usu�rio n�o encontrado.");

            await _usuarioService.UpdateUsuarioAsync(usuarioExistente.Id, usuarioViewModel);
            return Ok("Usu�rio atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var sucesso = await _usuarioService.DeleteUsuarioAsync(id);

            if (!sucesso)
                return NotFound("Usu�rio n�o encontrado.");

            return NoContent(); 
        }
    }
}

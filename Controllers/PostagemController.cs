using Microsoft.AspNetCore.Mvc;
using SocialApp.Interfaces;
using SocialApp.ViewModels;

namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemService _postagemService;

        public PostagemController(IPostagemService postagemService)
        {
            _postagemService = postagemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostagemViewModel>>> GetAll()
        {
            var postagens = await _postagemService.GetAllPostagensAsync();
            return Ok(postagens);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PostagemViewModel>> GetById(int id)
        {
            var postagem = await _postagemService.GetPostagemByIdAsync(id);

            if (postagem == null)
            {
                return NotFound();
            }

            return postagem;
        }

        [HttpPost]
        public async Task<ActionResult<PostagemViewModel>> Create(PostagemViewModel postagem)
        {
            try
            {
                if (postagem == null)
                {
                    return BadRequest("Dados da postagem não fornecidos.");
                }

                if (postagem.UsuarioID == 0 || postagem.TemaID == 0)
                {
                    return BadRequest("IDs de usuário e tema são obrigatórios.");
                }

                //var usuario = await _postagemService.GetUsuarioByIdAsync(postagem.UsuarioID);
                //var tema = await _postagemService.GetTemaByIdAsync(postagem.TemaID);
                //if (usuario == null || tema == null)
                //{
                //    return BadRequest("Usuário ou tema não encontrado.");
                //}

                if (string.IsNullOrEmpty(postagem.Titulo) || postagem.Titulo.Length < 3)
                {
                    return BadRequest("O título é obrigatório e deve ter no mínimo 3 caracteres.");
                }

                if (string.IsNullOrEmpty(postagem.Texto) || postagem.Texto.Length < 10)
                {
                    return BadRequest("O texto é obrigatório e deve ter no mínimo 10 caracteres.");
                }

                postagem.Data = DateTime.UtcNow;
                //postagem.Usuario = usuario;
                //postagem.Tema = tema;

                // Chamada ao repositório para criar a postagem
                var createdPostagem = await _postagemService.CreatePostagemAsync(postagem);
                return CreatedAtAction(nameof(GetById), new { id = createdPostagem.ID }, createdPostagem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostagem(int id, PostagemViewModel postagem)
        {
            if (id != postagem.ID)
            {
                return BadRequest();
            }

            try
            {
                await _postagemService.UpdatePostagemAsync(postagem);
            }
            catch (PostagemNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostagem(int id)
        {
            try
            {
                await _postagemService.DeletePostagemAsync(id);
            }
            catch (PostagemNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

            return NoContent();
        }
    }
}

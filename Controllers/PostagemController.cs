using Microsoft.AspNetCore.Mvc;
using SocialApp.Interfaces;
using SocialApp.model;
using SocialApp.ViewModels;

namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemService _postagemService;
        private readonly ITemaService _temaService;
        private readonly IUsuarioService _usuarioService;

        public PostagemController(IPostagemService postagemService, ITemaService temaService, IUsuarioService usuarioService)
        {
            _postagemService = postagemService;
            _temaService = temaService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostagemOutputViewModel>>> GetAllPostagens()
        {
            var postagens = await _postagemService.GetAllPostagensAsync();

            if (postagens == null || !postagens.Any())
                return NoContent();

            return Ok(postagens);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PostagemOutputViewModel>> GetPostagmById(int id)
        {
            var postagem = await _postagemService.GetPostagemByIdAsync(id);

            if (postagem == null)
                return NotFound("Postagem não encontrada");

            return postagem;
        }

        [HttpPost]
        public async Task<ActionResult<PostagemOutputViewModel>> PostPostagem(PostagemInputViewModel postagemViewModel)
        {
            if (postagemViewModel.UsuarioId == 0 || postagemViewModel.TemaId == 0)
                return BadRequest("Ids de usuário e tema são obrigatórios.");

            var usuario = await _usuarioService.GetUsuarioByIdAsync(postagemViewModel.UsuarioId);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            var tema = await _temaService.GetTemaByIdAsync(postagemViewModel.TemaId);
            if (tema == null)
                return NotFound("Tema não encontrado.");

            var createdPostagem = await _postagemService.CreatePostagemAsync(postagemViewModel);
            if(createdPostagem == null)
                return BadRequest("Erro ao criar postagem.");

            return CreatedAtAction(nameof(GetPostagmById), new { id = createdPostagem.Id }, createdPostagem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostagem(int id, PostagemInputViewModel postagemViewModel)
        {
            if (id != postagemViewModel.UsuarioId)
                return BadRequest("O Id informado não corresponde ao usuário.");

            var postagemExistente = await _postagemService.GetPostagemByIdAsync(id);

            if (postagemExistente == null)
                return NotFound("Postagem, não encontrada.");

            await _postagemService.UpdatePostagemAsync(id, postagemViewModel);
            return Ok("Postagem atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostagem(int id)
        {
            var sucesso = await _postagemService.DeletePostagemAsync(id);

            if (!sucesso)
                return NotFound("Postagem não encontrada.");

            return NoContent();
        }
    }
}

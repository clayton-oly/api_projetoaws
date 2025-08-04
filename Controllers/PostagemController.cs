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

        public PostagemController(IPostagemService postagemService)
        {
            _postagemService = postagemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostagemViewModel>>> GetAllPostagens()
        {
            var postagens = await _postagemService.GetAllPostagensAsync();

            if (postagens == null || !postagens.Any())
                return NoContent();

            return Ok(postagens);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PostagemViewModel>> GetPostagmById(int id)
        {
            var postagem = await _postagemService.GetPostagemByIdAsync(id);

            if (postagem == null)
                return NotFound("Postagem não encontrada");

            return postagem;
        }

        [HttpPost]
        public async Task<ActionResult<PostagemViewModel>> PostPostagem(PostagemViewModel postagemViewModel)
        {
            if (postagemViewModel == null)
            {
                return BadRequest("Dados da postagem não fornecidos.");
            }

            if (postagemViewModel.UsuarioID == 0 || postagemViewModel.TemaID == 0)
            {
                return BadRequest("IDs de usuário e tema são obrigatórios.");
            }

            //var usuario = await _postagemService.GetUsuarioByIdAsync(postagem.UsuarioID);
            //var tema = await _postagemService.GetTemaByIdAsync(postagem.TemaID);
            //if (usuario == null || tema == null)
            //{
            //    return BadRequest("Usuário ou tema não encontrado.");
            //}

            if (string.IsNullOrEmpty(postagemViewModel.Titulo) || postagemViewModel.Titulo.Length < 3)
            {
                return BadRequest("O título é obrigatório e deve ter no mínimo 3 caracteres.");
            }

            if (string.IsNullOrEmpty(postagemViewModel.Texto) || postagemViewModel.Texto.Length < 10)
            {
                return BadRequest("O texto é obrigatório e deve ter no mínimo 10 caracteres.");
            }

            postagemViewModel.Data = DateTime.UtcNow;
            //postagem.Usuario = usuario;
            //postagem.Tema = tema;

            var createdPostagem = await _postagemService.CreatePostagemAsync(postagemViewModel);
            if(createdPostagem == null)
                return BadRequest("Erro ao criar postagem.");

            return CreatedAtAction(nameof(GetPostagmById), new { id = createdPostagem.Id }, createdPostagem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostagem(int id, PostagemViewModel postagemViewModel)
        {
            if (id != postagemViewModel.Id)
                return BadRequest("O Id informado não corresponde ao usuário.");

            var postagemExistente = await _postagemService.GetPostagemByIdAsync(id);

            if (postagemExistente == null)
                return NotFound("Postagem, não encontrada.");

            await _postagemService.UpdatePostagemAsync(postagemViewModel);
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

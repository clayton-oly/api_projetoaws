using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrioConnect.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TrioConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        private readonly PostagemRepository _postagemRepository;

        public PostagemController(PostagemRepository postagemRepository)
        {
            _postagemRepository = postagemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Postagem>>> GetPostagens()
        {
            var postagens = await _postagemRepository.GetAllPostagensAsync();
            return Ok(postagens);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Postagem>> GetPostagem(int id)
        {
            var postagem = await _postagemRepository.GetPostagemByIdAsync(id);

            if (postagem == null)
            {
                return NotFound();
            }

            return postagem;
        }

        [HttpPost]
        public async Task<ActionResult<Postagem>> CriarPostagem(Postagem postagem)
        {
            try
            {
                // Suas valida��es existentes
                if (postagem == null)
                {
                    return BadRequest("Dados da postagem n�o fornecidos.");
                }

                if (postagem.UsuarioID == 0 || postagem.TemaID == 0)
                {
                    return BadRequest("IDs de usu�rio e tema s�o obrigat�rios.");
                }

                var usuario = await _postagemRepository.GetUsuarioByIdAsync(postagem.UsuarioID);
                var tema = await _postagemRepository.GetTemaByIdAsync(postagem.TemaID);
                if (usuario == null || tema == null)
                {
                    return BadRequest("Usu�rio ou tema n�o encontrado.");
                }

                if (string.IsNullOrEmpty(postagem.Titulo) || postagem.Titulo.Length < 3)
                {
                    return BadRequest("O t�tulo � obrigat�rio e deve ter no m�nimo 3 caracteres.");
                }

                if (string.IsNullOrEmpty(postagem.Texto) || postagem.Texto.Length < 10)
                {
                    return BadRequest("O texto � obrigat�rio e deve ter no m�nimo 10 caracteres.");
                }

                postagem.Data = DateTime.UtcNow;
                postagem.Usuario = usuario;
                postagem.Tema = tema;

                // Chamada ao reposit�rio para criar a postagem
                var createdPostagem = await _postagemRepository.CreatePostagemAsync(postagem);
                return CreatedAtAction(nameof(GetPostagem), new { id = createdPostagem.ID }, createdPostagem);
            }
            catch (Exception ex)
            {
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

            try
            {
                await _postagemRepository.UpdatePostagemAsync(postagem);
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
                await _postagemRepository.DeletePostagemAsync(id);
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

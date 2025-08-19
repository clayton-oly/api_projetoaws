using Microsoft.AspNetCore.Mvc;
using SocialApp.Interfaces;
using SocialApp.Services;
using SocialApp.ViewModels;

namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemaController : ControllerBase
    {
        private readonly ITemaService _temaService;

        public TemaController(ITemaService temaService)
        {
            _temaService = temaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemaViewModel>>> GetAllTemas()
        {
            var temas = await _temaService.GetAllTemasAsync();

            if (temas == null || !temas.Any())
                return NoContent();

            return Ok(temas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemaViewModel>> GetTemaById(int id)
        {
            var tema = await _temaService.GetTemaByIdAsync(id);

            if (tema == null)
                return NotFound("Tema não encontrado.");

            return tema;
        }

        [HttpPost]
        public async Task<ActionResult<TemaViewModel>> PostTema(TemaViewModel tema)
        {
            var createdTema = await _temaService.CreateTemaAsync(tema);

            if (createdTema == null)
                return BadRequest("Erro ao criar tema.");

            return CreatedAtAction(nameof(GetTemaById), new { id = createdTema.Id }, createdTema);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTema(int id, TemaViewModel temaViewModel)
        {
            if (id != temaViewModel.Id)
                return BadRequest("O id informado não corresponde ao tema.");

            var temaExistente = await _temaService.GetTemaByIdAsync(id);

            if (temaExistente == null)
                return NotFound("Usuário não encontrado.");

            await _temaService.UpdateTemaAsync(id, temaViewModel);
            return Ok("Tema atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTema(int id)
        {
            var sucesso = await _temaService.DeleteTemaAsync(id);

            if (!sucesso)
                return NotFound("Tema não encontrado.");

            return NoContent();
        }
    }
}

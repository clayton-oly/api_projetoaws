using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrioConnect.Data;
using TrioConnect.model;

namespace TrioConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemaController : ControllerBase
    {
        private readonly TemaRepository _temaRepository;

        public TemaController(TemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tema>>> GetTemas()
        {
            var temas = await _temaRepository.GetAllTemasAsync();
            return Ok(temas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tema>> GetTema(int id)
        {
            var tema = await _temaRepository.GetTemaByIdAsync(id);

            if (tema == null)
            {
                return NotFound();
            }

            return tema;
        }

        [HttpPost]
        public async Task<ActionResult<Tema>> PostTema(Tema tema)
        {
            try
            {
                var createdTema = await _temaRepository.CreateTemaAsync(tema);
                return CreatedAtAction(nameof(GetTema), new { id = createdTema.ID }, createdTema);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTema(int id, Tema tema)
        {
            if (id != tema.ID)
            {
                return BadRequest();
            }

            try
            {
                await _temaRepository.UpdateTemaAsync(tema);
            }
            catch (TemaNotFoundException)
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
        public async Task<IActionResult> DeleteTema(int id)
        {
            try
            {
                await _temaRepository.DeleteTemaAsync(id);
            }
            catch (TemaNotFoundException)
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

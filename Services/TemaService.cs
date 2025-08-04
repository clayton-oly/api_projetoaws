using Microsoft.AspNetCore.Mvc;
using SocialApp.Interfaces;
using SocialApp.model;
using SocialApp.ViewModels;

namespace SocialApp.Services
{
    public class TemaService : ITemaService
    {
        private readonly ITemaRepository _temaRepository;

        public TemaService(ITemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }

        private TemaViewModel MapToViewModel(Tema tema)
        {
            return new TemaViewModel
            {
                ID = tema.ID,
                Descricao = tema.Descricao
            };
        }

        private Tema MapToModel(TemaViewModel temaViewModel)
        {
            return new Tema
            {
                ID = temaViewModel.ID,
                Descricao = temaViewModel.Descricao
            };
        }

        public async Task<ActionResult<IEnumerable<TemaViewModel>>> GetAllTemasAsync()
        {
            var temas = await _temaRepository.GetAllTemasAsync();
            return temas.Select(MapToViewModel).ToList();
        }

        public async Task<ActionResult<TemaViewModel>> GetTemaByIdAsync(int id)
        {
            var tema = await _temaRepository.GetTemaByIdAsync(id);
            return MapToViewModel(tema);
        }

        public async Task<ActionResult<TemaViewModel>> CreateTemaAsync(TemaViewModel temaViewModel)
        {
            var tema = MapToModel(temaViewModel);
            var createdTema = await _temaRepository.CreateTemaAsync(tema);

            return MapToViewModel(createdTema);
        }

        public async Task UpdateTemaAsync(int id, TemaViewModel temaViewModel)
        {
            var tema = MapToModel(temaViewModel);
            await _temaRepository.UpdateTemaAsync(tema);
        }

        public async Task DeleteTemaAsync(int id)
        {
            await _temaRepository.DeleteTemaAsync(id);
        }
    }
}

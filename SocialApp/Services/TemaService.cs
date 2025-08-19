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
                Id = tema.ID,
                Descricao = tema.Descricao
            };
        }

        private Tema MapToModel(TemaViewModel temaViewModel)
        {
            return new Tema
            {
                ID = temaViewModel.Id,
                Descricao = temaViewModel.Descricao
            };
        }

        public async Task<IEnumerable<TemaViewModel>> GetAllTemasAsync()
        {
            var temas = await _temaRepository.GetAllTemasAsync() ?? new List<Tema>();
            return temas.Select(MapToViewModel).ToList();
        }

        public async Task<TemaViewModel> GetTemaByIdAsync(int id)
        {
            return await _temaRepository.GetTemaByIdAsync(id) is { } tema
                ? MapToViewModel(tema)
                : null;
        }

        public async Task<TemaViewModel> CreateTemaAsync(TemaViewModel temaViewModel)
        {
            var tema = MapToModel(temaViewModel);

            var createdTema = await _temaRepository.CreateTemaAsync(tema);

            return createdTema != null
                ? MapToViewModel(createdTema)
                : null;
        }

        public async Task UpdateTemaAsync(int id, TemaViewModel temaViewModel)
        {
            var tema = MapToModel(temaViewModel);

            await _temaRepository.UpdateTemaAsync(tema);
        }

        public async Task<bool> DeleteTemaAsync(int id)
        {
            return await _temaRepository.DeleteTemaAsync(id);
        }
    }
}

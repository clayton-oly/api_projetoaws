using SocialApp.Interfaces;
using SocialApp.model;
using SocialApp.ViewModels;
using System.Net.Sockets;

namespace SocialApp.Services
{
    public class PostagemService : IPostagemService
    {
        private readonly IPostagemRepository _postagemRepository;

        public PostagemService(IPostagemRepository postagemRepository)
        {
            _postagemRepository = postagemRepository;
        }

        private PostagemViewModel MapToViewModel(Postagem postagem)
        {
            return new PostagemViewModel
            {
                ID = postagem.ID,
                Titulo = postagem.Titulo,
                Texto = postagem.Texto
            };
        }

        private Postagem MapToModel(PostagemViewModel postagemViewModel)
        {
            return new Postagem
            {
                ID = postagemViewModel.ID,
                Titulo = postagemViewModel.Titulo,
                Texto = postagemViewModel.Texto
            };
        }

        public async Task<IEnumerable<PostagemViewModel>> GetAllPostagensAsync()
        {
            var postagens = await _postagemRepository.GetAllPostagensAsync();
            return postagens.Select(MapToViewModel).ToList();
        }

        public async Task<PostagemViewModel> GetPostagemByIdAsync(int id)
        {
            var postagem = await _postagemRepository.GetPostagemByIdAsync(id);
            return MapToViewModel(postagem);
        }

        public async Task<PostagemViewModel> CreatePostagemAsync(PostagemViewModel postagemViewModel)
        {
            var postagem = MapToModel(postagemViewModel);
            postagem = await _postagemRepository.CreatePostagemAsync(postagem);

            return MapToViewModel(postagem);
        }

        public async Task UpdatePostagemAsync(PostagemViewModel postagemViewModel)
        {
            var postagem = MapToModel(postagemViewModel);
            await _postagemRepository.UpdatePostagemAsync(postagem);
        }

        public async Task DeletePostagemAsync(int id)
        {
            await _postagemRepository.DeletePostagemAsync(id);
        }
    }
}

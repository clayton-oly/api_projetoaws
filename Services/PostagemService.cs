using SocialApp.Interfaces;
using SocialApp.model;
using SocialApp.ViewModels;

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
            var postagemViewModel = new PostagemViewModel
            {
                Id = postagem.ID,
                Titulo = postagem.Titulo,
                Texto = postagem.Texto,
                TemaId = postagem.TemaID,
                Usuario = new UsuarioOutputViewModel
                {
                    Nome = postagem.Usuario.Nome,
                    Foto = postagem.Usuario.Foto
                }
            };

            return postagemViewModel;
        }

        private Postagem MapToModel(PostagemViewModel postagemViewModel)
        {
            return new Postagem
            {
                ID = postagemViewModel.Id,
                Titulo = postagemViewModel.Titulo,
                Texto = postagemViewModel.Texto,
                TemaID = postagemViewModel.TemaId,
            };
        }

        public async Task<IEnumerable<PostagemViewModel>> GetAllPostagensAsync()
        {
            var postagens = await _postagemRepository.GetAllPostagensAsync() ?? new List<Postagem>();
            return postagens.Select(MapToViewModel).ToList();
        }

        public async Task<PostagemViewModel> GetPostagemByIdAsync(int id)
        {
            return await _postagemRepository.GetPostagemByIdAsync(id) is { } postagem
                ? MapToViewModel(postagem)
                : null;
        }

        public async Task<PostagemViewModel> CreatePostagemAsync(PostagemViewModel postagemViewModel)
        {
            var postagem = MapToModel(postagemViewModel);

            var postagemCreate = await _postagemRepository.CreatePostagemAsync(postagem);

            return postagemCreate != null
                ? MapToViewModel(postagemCreate)
                : null;
        }

        public async Task UpdatePostagemAsync(PostagemViewModel postagemViewModel)
        {
            var postagem = MapToModel(postagemViewModel);

            await _postagemRepository.UpdatePostagemAsync(postagem);
        }

        public async Task<bool> DeletePostagemAsync(int id)
        {
            return await _postagemRepository.DeletePostagemAsync(id);
        }
    }
}

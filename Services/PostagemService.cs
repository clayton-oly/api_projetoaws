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

        private PostagemOutputViewModel MapToViewModel(Postagem postagem)
        {
            var postagemViewModel = new PostagemOutputViewModel
            {
                Id = postagem.ID,
                Titulo = postagem.Titulo,
                Texto = postagem.Texto,
                TemaId = postagem.TemaID,
                Data = postagem.Data.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                Usuario = new UsuarioOutputViewModel
                {
                    Nome = postagem.Usuario.Nome,
                    Foto = postagem.Usuario.Foto
                }
            };

            return postagemViewModel;
        }

        private Postagem MapToModel(PostagemInputViewModel postagemViewModel)
        {
            return new Postagem
            {
                Titulo = postagemViewModel.Titulo,
                Texto = postagemViewModel.Texto,
                TemaID = postagemViewModel.TemaId,
                UsuarioID = postagemViewModel.UsuarioId,
            };
        }

        public async Task<IEnumerable<PostagemOutputViewModel>> GetAllPostagensAsync()
        {
            var postagens = await _postagemRepository.GetAllPostagensAsync() ?? new List<Postagem>();
            return postagens.Select(MapToViewModel).ToList();
        }

        public async Task<PostagemOutputViewModel> GetPostagemByIdAsync(int id)
        {
            return await _postagemRepository.GetPostagemByIdAsync(id) is { } postagem
                ? MapToViewModel(postagem)
                : null;
        }

        public async Task<PostagemOutputViewModel> CreatePostagemAsync(PostagemInputViewModel postagemViewModel)
        {
            var postagem = MapToModel(postagemViewModel);

            var postagemCreate = await _postagemRepository.CreatePostagemAsync(postagem);

            return postagemCreate != null
                ? MapToViewModel(postagemCreate)
                : null;
        }

        public async Task UpdatePostagemAsync(int id, PostagemInputViewModel postagemViewModel)
        {
            var postagem = MapToModel(postagemViewModel);
            postagem.ID = id;

            await _postagemRepository.UpdatePostagemAsync(postagem);
        }

        public async Task<bool> DeletePostagemAsync(int id)
        {
            return await _postagemRepository.DeletePostagemAsync(id);
        }
    }
}

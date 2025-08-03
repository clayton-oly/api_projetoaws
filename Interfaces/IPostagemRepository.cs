using SocialApp.model;

namespace SocialApp.Interfaces
{
    public interface IPostagemRepository
    {
        Task<IEnumerable<Postagem>> GetAllPostagensAsync();

        Task<Postagem> GetPostagemByIdAsync(int id);

        Task<Postagem> CreatePostagemAsync(Postagem postagem);

        Task UpdatePostagemAsync(Postagem postagem);

        Task DeletePostagemAsync(int id);

        Task<Usuario> GetUsuarioByIdAsync(int id);

        Task<Tema> GetTemaByIdAsync(int id);

    }
}

using SocialApp.model;

namespace SocialApp.Interfaces
{
    public interface IPostagemRepository
    {
        Task<IEnumerable<Postagem>> GetAllPostagensAsync();
        Task<Postagem> GetPostagemByIdAsync(int id);
        Task<Postagem> CreatePostagemAsync(Postagem postagem);
        Task UpdatePostagemAsync(Postagem postagem);
        Task<bool> DeletePostagemAsync(int id);
    }
}

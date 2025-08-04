using SocialApp.ViewModels;

namespace SocialApp.Interfaces
{
    public interface IPostagemService
    {
        Task<IEnumerable<PostagemViewModel>> GetAllPostagensAsync();
        Task<PostagemViewModel> GetPostagemByIdAsync(int id);
        Task<PostagemViewModel> CreatePostagemAsync(PostagemViewModel postagemViewModel);
        Task UpdatePostagemAsync(PostagemViewModel postagemViewModel);
        Task<bool> DeletePostagemAsync(int id);
    }
}

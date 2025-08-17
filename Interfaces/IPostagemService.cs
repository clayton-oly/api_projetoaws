using SocialApp.ViewModels;

namespace SocialApp.Interfaces
{
    public interface IPostagemService
    {
        Task<IEnumerable<PostagemOutputViewModel>> GetAllPostagensAsync();
        Task<PostagemOutputViewModel> GetPostagemByIdAsync(int id);
        Task<PostagemOutputViewModel> CreatePostagemAsync(PostagemInputViewModel postagemViewModel);
        Task UpdatePostagemAsync(int id, PostagemInputViewModel postagemViewModel);
        Task<bool> DeletePostagemAsync(int id);
    }
}

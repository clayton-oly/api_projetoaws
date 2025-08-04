using SocialApp.model;
using SocialApp.ViewModels;

namespace SocialApp.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioViewModel>> GetAllUsuariosAsync();
        Task<UsuarioViewModel> GetUsuarioByIdAsync(int id);
        Task<UsuarioViewModel> CreateUsuarioAsync(UsuarioViewModel usuarioViewModel);
        Task UpdateUsuarioAsync(int id, UsuarioViewModel usuarioViewModel);
        Task DeleteUsuarioAsync(int id);
    }
}

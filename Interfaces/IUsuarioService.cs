using SocialApp.ViewModels;

namespace SocialApp.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioOutputViewModel>> GetAllUsuariosAsync();
        Task<UsuarioOutputViewModel> GetUsuarioByIdAsync(int id);
        Task<UsuarioOutputViewModel> CreateUsuarioAsync(UsuarioInputViewModel usuarioViewModel);
        Task UpdateUsuarioAsync(int id, UsuarioInputViewModel usuarioViewModel);
        Task<bool> DeleteUsuarioAsync(int id);
    }
}

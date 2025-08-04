using SocialApp.ViewModels;

namespace SocialApp.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioViewModel>> GetAllUsuariosAsync();
        Task<UsuarioViewModel> GetUsuarioByIdAsync(int id);
        Task<UsuarioViewModel> CreateUsuarioAsync(UsuarioViewModel usuarioViewModel);
        Task UpdateUsuarioAsync(int id, UsuarioViewModel usuarioViewModel);
        Task<bool> DeleteUsuarioAsync(int id);
    }
}

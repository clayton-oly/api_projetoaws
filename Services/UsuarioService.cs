using SocialApp.Interfaces;
using SocialApp.model;
using SocialApp.ViewModels;

namespace SocialApp.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        private UsuarioViewModel MapToViewModel(Usuario usuario)
        {
            return new UsuarioViewModel
            {
                Id = usuario.ID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Foto = usuario.Foto
            };
        }

        private Usuario MapToModel(UsuarioViewModel usuarioViewModel)
        {
            return new Usuario
            {
                ID = usuarioViewModel.Id,
                Nome = usuarioViewModel.Nome,
                Email = usuarioViewModel.Email,
                Foto = usuarioViewModel.Foto
            };
        }

        public async Task<IEnumerable<UsuarioViewModel>> GetAllUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetAllUsuariosAsync() ?? new List<Usuario>();
            return usuarios.Select(MapToViewModel).ToList();
        }

        public async Task<UsuarioViewModel> GetUsuarioByIdAsync(int id)
        {
            return await _usuarioRepository.GetUsuarioByIdAsync(id) is { } usuario
                ? MapToViewModel(usuario)
                : null;
        }

        public async Task<UsuarioViewModel> CreateUsuarioAsync(UsuarioViewModel usuarioViewModel)
        {
            var usuario = MapToModel(usuarioViewModel);

            var usuarioCreate = await _usuarioRepository.CreateUsuarioAsync(usuario);

            return usuarioCreate != null
                ? MapToViewModel(usuarioCreate)
                : null;
        }

        public async Task UpdateUsuarioAsync(UsuarioViewModel usuarioViewModel)
        {
            var usuario = MapToModel(usuarioViewModel);

            await _usuarioRepository.UpdateUsuarioAsync(usuario);
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            return await _usuarioRepository.DeleteUsuarioAsync(id);
        }
    }
}

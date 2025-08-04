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
                ID = usuario.ID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Foto = usuario.Foto
            };
        }

        private Usuario MapToModel(UsuarioViewModel usuario)
        {
            return new Usuario
            {
                ID = usuario.ID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Foto = usuario.Foto
            };
        }

        public async Task<List<UsuarioViewModel>> GetAllUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetAllUsuariosAsync();
            return usuarios.Select(MapToViewModel).ToList();
        }

        public async Task<UsuarioViewModel> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);

            return MapToViewModel(usuario);
        }

        public async Task<UsuarioViewModel> CreateUsuarioAsync(UsuarioViewModel usuarioViewModel)
        {
            var usuario = MapToModel(usuarioViewModel);
            var createdUsuario = await _usuarioRepository.CreateUsuarioAsync(usuario);

            return MapToViewModel(createdUsuario);
        }

        public async Task UpdateUsuarioAsync(int id, UsuarioViewModel usuarioViewModel)
        {
            var usuario = MapToModel(usuarioViewModel);
            
            await _usuarioRepository.UpdateUsuarioAsync(usuario);
        }

        public async Task DeleteUsuarioAsync(int id)
        {
           await _usuarioRepository.DeleteUsuarioAsync(id);
        }
    }
}

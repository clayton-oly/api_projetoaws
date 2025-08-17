using System.ComponentModel.DataAnnotations;

namespace SocialApp.ViewModels
{
    public class PostagemOutputViewModel
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "O campo Título deve ter no mínimo 3 caracteres.")]
        public string? Titulo { get; set; }

        [MinLength(10, ErrorMessage = "O campo Texto deve ter no mínimo 10 caracteres.")]
        public string? Texto { get; set; }

        public string Data { get; set; }

        public UsuarioOutputViewModel Usuario { get; set; }

        public int TemaId { get; set; }
    }
}

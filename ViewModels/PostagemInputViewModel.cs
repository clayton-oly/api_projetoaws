using SocialApp.model;
using System.ComponentModel.DataAnnotations;

namespace SocialApp.ViewModels
{
    public class PostagemInputViewModel
    {
        [MinLength(3, ErrorMessage = "O campo Título deve ter no mínimo 3 caracteres.")]
        public string? Titulo { get; set; }

        [MinLength(10, ErrorMessage = "O campo Texto deve ter no mínimo 10 caracteres.")]
        public string? Texto { get; set; }

        public int UsuarioId { get; set; }

        public int TemaId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialApp.ViewModels
{
    public class UsuarioViewModel
    {
        public int ID { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Foto { get; set; }

        public ICollection<Postagem>? Postagens { get; set; }
    }
}

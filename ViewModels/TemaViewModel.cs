using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialApp.ViewModels
{
    public class TemaViewModel
    {
        public int ID { get; set; }
        public string? Descricao { get; set; }

        public ICollection<Postagem>? Postagens { get; set; }
    }
}

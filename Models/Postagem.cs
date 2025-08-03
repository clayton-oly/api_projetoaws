using SocialApp.model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialApp
{
    [Table("tb_postagens")]
    public class Postagem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MinLength(3, ErrorMessage = "O campo Título deve ter no mínimo 3 caracteres.")]
        public string? Titulo { get; set; }

        [MinLength(10, ErrorMessage = "O campo Texto deve ter no mínimo 10 caracteres.")]
        public string? Texto { get; set; }

        public DateTime Data { get; set; }

        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }

        public int TemaID { get; set; }
        public Tema? Tema { get; set; }


    }
}

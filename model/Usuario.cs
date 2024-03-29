using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrioConnect.model
{
    [Table("tb_usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Foto { get; set; }

        public ICollection<Postagem>? Postagens { get; set; }
    }
}

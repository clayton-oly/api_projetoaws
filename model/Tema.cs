﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrioConnect.model
{
    [Table("tb_temas")]
    public class Tema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Descricao { get; set; }

        public ICollection<Postagem>? Postagens { get; set; }
    }
}

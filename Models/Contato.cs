using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniControl.Models
{
    public partial class Contato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string Telefone { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}

using Domain.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    [BsonCollection("livro")]
    public class Livro : Document
    {
        [Required]
        public string Titulo { get; set; }
        public bool Alugado { get; set; }
        public DateTime DataCadastro { get; set; }

        public long? IdCliente { get; set; }
        public Cliente Cliente { get; set; }
    }
}

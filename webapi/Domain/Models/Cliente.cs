using Domain.Utils;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    [BsonCollection("cliente")]
    public class Cliente : Document
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

﻿using Domain.Utils;

namespace Domain.Models
{
    [BsonCollection("livro")]
    public class Livro : Document
    {
        public string Titulo { get; set; }
        public bool Alugado { get; set; }

        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
    }
}

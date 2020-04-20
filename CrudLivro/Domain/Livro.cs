using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool Alugado { get; set; }

        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
    }
}

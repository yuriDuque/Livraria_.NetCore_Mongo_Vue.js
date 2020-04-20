using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        public List<Livro> Livros { get; set; } = new List<Livro>();
    }
}

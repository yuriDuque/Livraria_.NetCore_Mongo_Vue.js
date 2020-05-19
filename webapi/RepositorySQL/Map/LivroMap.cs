using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositorySQL.Map
{
    public class LivroMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var map = modelBuilder.Entity<Livro>();

            map.HasKey(x => x.Id);
            map.Property(x => x.Id).ValueGeneratedOnAdd();

            map.HasOne(x => x.Cliente).WithMany(x => x.Livros).HasForeignKey(x => x.IdCliente);
        }
    }
}

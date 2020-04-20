using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace RepositorySQL.Map
{
    public class ClienteMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var map = modelBuilder.Entity<Cliente>();

            map.HasKey(x => x.Id);
            map.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}

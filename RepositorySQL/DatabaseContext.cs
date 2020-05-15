using Domain;
using Microsoft.EntityFrameworkCore;
using RepositorySQL.Map;

namespace RepositorySQL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> option)
            : base(option)
        { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ServerLocal;Database=GerenciamentoProdutos;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ClienteMap.Map(modelBuilder);
            LivroMap.Map(modelBuilder);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Livro> Livros { get; set; }
    }
}

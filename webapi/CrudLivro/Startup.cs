using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryMongo;
using RepositoryMongo.Repository;
using RepositoryMongo.Repository.ModelsRepository;
using Service.ModelsService;

namespace CrudLivro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            // Singleton - cria uma instancia e toda vez que for chamada, usará a mesma instancia
            services.AddSingleton<MongoContext>();

            #region Repository

            // Scoped - cria uma instancia por requisição dentro do escopo
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddScoped<ClienteRepository>();
            services.AddScoped<LivroRepository>();

            #endregion

            #region Service

            // Transiente - cria um objeto do servico toda vez que um objeto for requisitado
            services.AddTransient(typeof(ILivroService), typeof(LivroService));
            services.AddTransient(typeof(IClienteService), typeof(ClienteService));

            #endregion

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

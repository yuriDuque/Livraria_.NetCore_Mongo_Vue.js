using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RepositoryMongo;
using RepositoryMongo.Repository;
using RepositoryMongo.Repository.ModelsRepository;
using Service.ModelsService;
using System.Text;

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

            #region Token

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key), // chave de assinatura do emissor
                    ValidateIssuerSigningKey = true, // verifica se dentro do token o emissor é o que foi informado

                    ValidIssuer = appSettings.Emissor,
                    ValidateIssuer = true, // valida o emissor

                    ValidAudience = appSettings.ValidoEm,
                    ValidateAudience = true, // valida se o token é valido em "validoEm"
                };
            });

            #endregion

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

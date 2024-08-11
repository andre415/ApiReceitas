

using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Infrastructure.AcessoRepo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;

public class MeuLivroDeReceitasWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
{
    private MeuLivroDeReceitas.Domain.Entidades.Usuario _usuario;
    private string _senha;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test").ConfigureServices(services =>
        {
          var descritor = services.SingleOrDefault(d => d.ServiceType == typeof(MeuLivroDeReceitasContext));
          var provider= services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

          if (descritor != null) services.Remove(descritor);  
            services.AddDbContext<MeuLivroDeReceitasContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbforTesting");
                options.UseInternalServiceProvider(provider);
            });

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var dataBase = scope.ServiceProvider.GetRequiredService<MeuLivroDeReceitasContext>();

            dataBase.Database.EnsureDeleted();
            (_usuario, _senha) = ContextSeedInMemory.Seed(dataBase);
        });
    }
    public MeuLivroDeReceitas.Domain.Entidades.Usuario RetornarUsuario()
    {
        return _usuario;
    }

    public string RetornarSenha()
    {
        return _senha;
    }
}

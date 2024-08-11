using FluentMigrator.Runner;
using MeuLivroDeReceitas.Domain.Extensions;
using MeuLivroDeReceitas.Domain.Repositorio;
using MeuLivroDeReceitas.Infrastructure.AcessoRepo;
using MeuLivroDeReceitas.Infrastructure.AcessoRepo.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using MeuLivroDeReceitas.Domain.Repositorio.Receita;
using MeuLivroDeReceitas.Domain.Repositorio.Codigo;
using MeuLivroDeReceitas.Domain.Repositorio.Conexao;

namespace MeuLivroDeReceitas.Infrastructure;

public static class bootstrapper
{
    public static void AddRepositorio(this IServiceCollection services, IConfiguration configuration)
    {
        AddFluentMigrator(services, configuration);
        AddRepositorios(services);
        AddUnidadeDeTrabalho(services);
        AddContexto(services, configuration);
    } 

    private static void AddContexto(IServiceCollection services , IConfiguration configuration)
    {
        bool.TryParse(configuration.GetSection("Configuracoes:BancoDeDadosInMemory").Value, out bool BancoDeDadosInMemory);

        if (!BancoDeDadosInMemory)
        {
            string conect = configuration.GetConaxaoCompleta();
            var versaoServidor = new MySqlServerVersion(new Version(5, 7, 36));
            services.AddDbContext<MeuLivroDeReceitasContext>(DbContextOptions =>
            {
                DbContextOptions.UseMySql(conect, versaoServidor);
            });
        }
       
    }
    private static void AddUnidadeDeTrabalho(IServiceCollection services)
    {
        services.AddScoped<IunidadeDeTrabalho, UnidadeDeTrabalho>();
    }
    private static void AddRepositorios(IServiceCollection services)
    {
        services.AddScoped<IRepositorioUsuarioReadOnly, UsuarioRepositorio>()
            .AddScoped<IRepositorioUsuarioWriteOnly, UsuarioRepositorio>()
            .AddScoped<IRepositorioUpdate, UsuarioRepositorio>()
            .AddScoped<IReceitasWriteOnlyRepositorio, ReceitaRepositorio>()
            .AddScoped<IReceitaReadOnlyRepo, ReceitaRepositorio>()
            .AddScoped<IReceitaUpadateOnlyRepositorio, ReceitaRepositorio>()
            .AddScoped<ICodigoWriteOnlyRepositorio, CodigoRepositorio>()
            .AddScoped<ICodigoReadOnlyRepositorio, CodigoRepositorio>()
            .AddScoped<IConexaoWriteOnly, ConexaoRepositorio>()
            .AddScoped<IConexaoReadOnly, ConexaoRepositorio>(); 


    }

    private static void AddFluentMigrator(IServiceCollection services , IConfiguration configuration)
    {
        bool.TryParse(configuration.GetSection("Configuracoes:BancoDeDadosInMemory").Value, out bool BancoDeDadosInMemory);

        if (!BancoDeDadosInMemory)
        {
         services.AddFluentMigratorCore().ConfigureRunner(c => c.AddMySql5().
         WithGlobalConnectionString(configuration.GetConaxaoCompleta())
         .ScanIn(Assembly.Load("MeuLivroDeReceitas.Infrastructure")).For.All()
        );
        }
            
    }
}

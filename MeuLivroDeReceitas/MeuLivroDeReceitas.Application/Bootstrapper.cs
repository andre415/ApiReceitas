using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Application.UseCases.Conexao;
using MeuLivroDeReceitas.Application.UseCases.Conexao.AceitarConexaoUseCase;
using MeuLivroDeReceitas.Application.UseCases.Conexao.QRCodeLidoUseCase;
using MeuLivroDeReceitas.Application.UseCases.Conexao.RecuperarTodasAsConexoesUseCase;
using MeuLivroDeReceitas.Application.UseCases.Conexao.RecusarConexaoUseCase;
using MeuLivroDeReceitas.Application.UseCases.Conexao.Remover;
using MeuLivroDeReceitas.Application.UseCases.DashBoard;
using MeuLivroDeReceitas.Application.UseCases.Login.FazerLogin;
using MeuLivroDeReceitas.Application.UseCases.Receitas;
using MeuLivroDeReceitas.Application.UseCases.Receitas.DeletarReceita;
using MeuLivroDeReceitas.Application.UseCases.Receitas.RecuperarPorId;
using MeuLivroDeReceitas.Application.UseCases.Receitas.Registar;
using MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Application.UseCases.Usuario.RecuperarPerfil;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeuLivroDeReceitas.Application;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AdicionarUseCases(services);
        AdicionarChaveAdicionalEToken(services, configuration);
        AdiconarUsuarioLogado(services);
    }

    private static void AdiconarUsuarioLogado(IServiceCollection services)
    {
        services.AddScoped<IUsuarioLogado, UsuarioLogado>();
    }

    public  static void AdicionarChaveAdicionalEToken(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(option => new EncriptadorDeSenha(GetConfig("ChaveAdicionalSenha", configuration)));
        services.AddScoped(option => new TokenControler(
           int.Parse(GetConfig("TempoDeVidaToken", configuration)), GetConfig("ChaveToken", configuration)));
    }
    public static  string  GetConfig(string valor , IConfiguration configuration)
    {
        return configuration.GetSection($"Configuracoes:{valor}").Value;
    }
    public static void AdicionarUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>()
            .AddScoped<ILoginUseCase, LoginUseCase >()
            .AddScoped<IAlterarSenhaUseCase, AlterarSenhaUseCase>()
            .AddScoped<IRegistrarReceitasUseCase, RegistrarReceitasUseCase>()
            .AddScoped<IDashBoardUseCase, DashBoardUseCase>()
            .AddScoped<IRecuperarReceitaPorIdUseCase, RecuperarReceitaPorIdUseCase>()
            .AddScoped<IAtualizarReceitaUseCase, AtualizarReceitaUseCase>()
            .AddScoped<IDeletarReceitaUseCase, DeletarReceitaUseCase>()
            .AddScoped<IRecuperarPerfilUseCase, RecuperarPerfilUseCase>()
            .AddScoped<IGerarQrCodeUseCase, GerarQrCodeUseCase>()
            .AddScoped<IQRCodeLidoUseCase, QRCodeLidoUseCase>()
            .AddScoped<IRecusarConexaoUseCase, RecusarConexaoUseCase>()
            .AddScoped<IAceitarConexaoUseCase, AceitarConexaoUseCase>()
            .AddScoped<IRecuperarTodasAsConexoesUseCase, RecuperarTodasAsConexoesUseCase>()
            .AddScoped<IRemoverConexaoUseCase, RemoverConexaoUseCase>();
    }
}

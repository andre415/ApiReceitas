using MeuLivroDeReceitas.Application.UseCases.Conexao;
using MeuLivroDeReceitas.Application.UseCases.Conexao.AceitarConexaoUseCase;
using MeuLivroDeReceitas.Application.UseCases.Conexao.QRCodeLidoUseCase;
using MeuLivroDeReceitas.Application.UseCases.Conexao.RecusarConexaoUseCase;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Excepetions;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace MeuLivroDeReceitas.API.WebSockets;

public class AdicionarConexao : Hub
{
    private readonly IRecusarConexaoUseCase _recusarConexaoUseCase;
    private readonly IQRCodeLidoUseCase _qRCodeLidoUseCase;
    private readonly IHubContext<AdicionarConexao> _hubContext;
    private readonly Broadcaster _broadcaster;
    private readonly IGerarQrCodeUseCase _gerarQrCodeUseCase;
    private readonly IAceitarConexaoUseCase _aceitarConexaoUseCase;
    public AdicionarConexao(IGerarQrCodeUseCase gerarQrCodeUseCase, 
        IHubContext<AdicionarConexao> hubContext,
        IQRCodeLidoUseCase qRCodeLidoUseCase,
        IRecusarConexaoUseCase recusarConexaoUseCase,
        IAceitarConexaoUseCase aceitarConexaoUseCase)
    {
        _aceitarConexaoUseCase = aceitarConexaoUseCase;
        _recusarConexaoUseCase = recusarConexaoUseCase;
        _broadcaster = Broadcaster.Instance;
        _gerarQrCodeUseCase = gerarQrCodeUseCase;
        _hubContext = hubContext;
        _qRCodeLidoUseCase = qRCodeLidoUseCase;

    }

    [Authorize(Policy = "UsuarioLogado")]

    public async Task GetQRCode()
    {
        try
        {
            (var qrCode, string IdUsuario) = await _gerarQrCodeUseCase.Executar();

            _broadcaster.InicializarConexao(_hubContext, Context.ConnectionId, IdUsuario);
            await Clients.Caller.SendAsync("Resultado QRCode", qrCode);
        }
        catch (MeuLivroDeReceitasExcepetion ex)
        {
            await Clients.Caller.SendAsync("Erro", ex.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
       
    }

    [Authorize(Policy = "UsuarioLogado")]
    public async Task QRCodeLido(string codigoConexao)
    {
        
        try
        {
           (var usuarioParaSeConectar,string idUsuarioQueGerouQrCode) = await _qRCodeLidoUseCase.Executar(codigoConexao);

            var connectionId = _broadcaster.GetConnectionIdDoUsuario(idUsuarioQueGerouQrCode);

            _broadcaster.ResetarTempoDeExpiracao(connectionId);
            _broadcaster.SetconnectionIdUsuarioQueLeuQRC(idUsuarioQueGerouQrCode, Context.ConnectionId);
            await Clients.Clients(connectionId).SendAsync("ResultadoQRCodeLido", usuarioParaSeConectar);
        }
        catch(MeuLivroDeReceitasExcepetion ex)
        {
            await Clients.Caller.SendAsync("Erro", ex.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
    }

    [Authorize(Policy = "UsuarioLogado")]
    public async Task RecusarConexao()
    {
        try
        {
            var usuarioId = await _recusarConexaoUseCase.Executar();

            var connectionIdLeitorQRCode = _broadcaster.Remover(Context.ConnectionId, usuarioId);

            await Clients.Client(connectionIdLeitorQRCode).SendAsync("OnConexaoRecusada");
        }
        catch (MeuLivroDeReceitasExcepetion ex)
        {
            await Clients.Caller.SendAsync("Erro", ex.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
        
    }
    [Authorize(Policy = "UsuarioLogado")]
    public async Task AceitarConexao(string idUsuarioParaSeConectar)
    {
        try
        {
            var usuarioId = await _aceitarConexaoUseCase.Executar(idUsuarioParaSeConectar);

        var connectionIdLeitorQRCode = _broadcaster.Remover(Context.ConnectionId, usuarioId);

        await Clients.Client(connectionIdLeitorQRCode).SendAsync("OnConexaoAceita");
        }
        catch (MeuLivroDeReceitasExcepetion ex)
        {
            await Clients.Caller.SendAsync("Erro", ex.Message);
        }
        catch(System.Exception ex)
        {
            await Clients.Caller.SendAsync("Erro", ResourceMensagensDeErro.ERRO_DESCONHECIDO);
        }
        
    }
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
}

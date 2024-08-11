using MeuLivroDeReceitas.Application.UseCases.Conexao.RecusarConexaoUseCase;
using MeuLivroDeReceitas.Excepetions.ExcepetionsBase;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MeuLivroDeReceitas.API.WebSockets;

public class Broadcaster 
{
    private readonly static Lazy<Broadcaster> _instance = new (() => new Broadcaster());
    public static Broadcaster Instance { get { return _instance.Value;  } }

    private ConcurrentDictionary<string, object> _dictionary { get; set; }

    public Broadcaster()
    {
        _dictionary = new ConcurrentDictionary<string, object>();
    }

    public void InicializarConexao(IHubContext<AdicionarConexao> hubContext, string connectionId, string IdUsuarioQRCode )
    {
        var conecao = new Conexao(hubContext,connectionId);

        _dictionary.TryAdd(connectionId, conecao);
        _dictionary.TryAdd(IdUsuarioQRCode, connectionId);

        conecao.IniciarContagemTempo(CallBackTempoExpirado);
    }

    private void CallBackTempoExpirado(string connectionId)
    {
        _dictionary.TryRemove(connectionId, out _);
    }
    
    public string GetConnectionIdDoUsuario(string usuarioId)
    {
        if(!_dictionary.TryGetValue(usuarioId, out var connectionId))
        {
            throw new MeuLivroDeReceitasExcepetion("");
        }

        return connectionId.ToString();
    }

    public void ResetarTempoDeExpiracao(string connectionId)
    {
        _dictionary.TryGetValue(connectionId, out var objConexao);

        var conexao = objConexao as Conexao;

        conexao.ResetarContagemDeTempo();
    }
    public void SetconnectionIdUsuarioQueLeuQRC( string idUsuarioQueGerouQRCode ,string connectionIdUsuarioLeitorQRCode )
    {
        var connectionIdUsuarioLeuQRCode = GetConnectionIdDoUsuario(idUsuarioQueGerouQRCode);

        _dictionary.TryGetValue(connectionIdUsuarioLeuQRCode, out var objConexao);

        var conexao = objConexao as Conexao;

        conexao.SetconnectionIdUsuarioQueLeuQRC(connectionIdUsuarioLeitorQRCode);
    }
    public string Remover(string connectionId, string usuarioId)
    {
        _dictionary.TryGetValue(connectionId, out var objConexao);

        var conexao = objConexao as Conexao;

        conexao.StopTimer();

        _dictionary.TryRemove(connectionId, out _);
        _dictionary.TryRemove(usuarioId, out _);

        return conexao.UsuarioQueLeuQRC();
    }
}

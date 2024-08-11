using Microsoft.AspNetCore.SignalR;
using System.Timers;

namespace MeuLivroDeReceitas.API.WebSockets;

public class Conexao
{
    private readonly IHubContext<AdicionarConexao> _hubContext;
    private readonly string _UsuarioQRCodeConnectionId;
    private Action<string> _callBackTempoExpirado;
    private string connectionIdUsuarioQueLeuQRC;
    public Conexao (IHubContext<AdicionarConexao> hubContext, string usuarioQRCodeConnectionId)
    {
        _hubContext = hubContext;
        _UsuarioQRCodeConnectionId = usuarioQRCodeConnectionId;
        
    }

    private short tempoRestanteSegundos { get; set; }
    private  System.Timers.Timer _timer;

    public void IniciarContagemTempo(Action<string> callBackTempoExpirado)
    {
        _callBackTempoExpirado = callBackTempoExpirado;
        StartTimer();
    }

    public void SetconnectionIdUsuarioQueLeuQRC(string connectionId)
    {
        connectionIdUsuarioQueLeuQRC = connectionId;
    }

    public string UsuarioQueLeuQRC()
    {
        return connectionIdUsuarioQueLeuQRC;
    }

    public void ResetarContagemDeTempo()
    {
        StopTimer();
        StartTimer();
    }

    private void StartTimer()
    {
        tempoRestanteSegundos = 600;
        _timer = new System.Timers.Timer(1000)
        {
            Enabled = false,
        };

        _timer.Elapsed += ElapsedTimer;
        _timer.Enabled = true;
    }

    public void StopTimer()
    {
        _timer?.Stop();
        _timer?.Dispose();
        _timer = null;
    }
    private  async void ElapsedTimer(object? sender, ElapsedEventArgs e)
    {
        if (tempoRestanteSegundos >= 0)
            await _hubContext.Clients.Client(_UsuarioQRCodeConnectionId).SendAsync("SetTempoRestante", tempoRestanteSegundos--);
        else
        {
            StopTimer();
            _callBackTempoExpirado(_UsuarioQRCodeConnectionId);
        }
    }
}
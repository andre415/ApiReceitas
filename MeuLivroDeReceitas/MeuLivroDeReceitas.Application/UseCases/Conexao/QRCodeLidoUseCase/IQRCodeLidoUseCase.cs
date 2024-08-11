using MeuLivroDeReceitas.Comunicacao.Respostas;

namespace MeuLivroDeReceitas.Application.UseCases.Conexao.QRCodeLidoUseCase;
public interface IQRCodeLidoUseCase
{
    Task<(RespostaConexaoUsuarioJson UsuarioConectado, string idUsuarioQueGerouQRCode)> Executar(string codigoConexao);
}

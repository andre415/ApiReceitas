namespace MeuLivroDeReceitas.Application.UseCases.Conexao;
public interface IGerarQrCodeUseCase
{
    Task<(string QRCode , string IdUsuario)> Executar();
}

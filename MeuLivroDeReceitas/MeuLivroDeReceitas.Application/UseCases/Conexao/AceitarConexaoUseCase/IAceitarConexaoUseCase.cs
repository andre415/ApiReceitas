namespace MeuLivroDeReceitas.Application.UseCases.Conexao.AceitarConexaoUseCase;
public interface IAceitarConexaoUseCase 
{
    Task<string> Executar(string idUsuarioParaSeConectar);
}

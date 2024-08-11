namespace MeuLivroDeReceitas.Application.UseCases.Receitas.DeletarReceita;
public interface IDeletarReceitaUseCase
{
    Task Executar(long id);
}

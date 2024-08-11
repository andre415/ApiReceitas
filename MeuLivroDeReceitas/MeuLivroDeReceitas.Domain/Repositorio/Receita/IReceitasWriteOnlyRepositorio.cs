namespace MeuLivroDeReceitas.Domain.Repositorio.Receita;

public interface IReceitasWriteOnlyRepositorio
{
    Task Registrar(Entidades.Receita receita);

    Task Deletar(long id);
}

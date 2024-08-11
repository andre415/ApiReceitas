namespace MeuLivroDeReceitas.Domain.Repositorio.Codigo;
public interface ICodigoReadOnlyRepositorio
{
    Task<Entidades.Codigos> RecuperarEntidadeCodigo(string codigo);
}

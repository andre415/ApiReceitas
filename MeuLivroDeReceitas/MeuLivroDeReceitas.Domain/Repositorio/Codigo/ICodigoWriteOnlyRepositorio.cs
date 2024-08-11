namespace MeuLivroDeReceitas.Domain.Repositorio.Codigo;
public interface ICodigoWriteOnlyRepositorio
{
    Task Registrar(Entidades.Codigos codigo);
    Task Deletar(long usuarioId);
}

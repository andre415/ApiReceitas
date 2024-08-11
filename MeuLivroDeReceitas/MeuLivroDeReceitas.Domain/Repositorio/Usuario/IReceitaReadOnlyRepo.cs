namespace MeuLivroDeReceitas.Domain.Repositorio.Usuario;
public interface IReceitaReadOnlyRepo
{
    Task <IList<Entidades.Receita>> RecuperarTodasDoUsuario(long usuarioId);

    Task<IList<Entidades.Receita>> RecuperarTodasDosUsuarios(List<long> usuariosIds);
    Task <Entidades.Receita> RecuperarReceitaPorId(long ReceitaId1);

    Task<int> QuantidadesDeReceitas(long idUsuario);
}

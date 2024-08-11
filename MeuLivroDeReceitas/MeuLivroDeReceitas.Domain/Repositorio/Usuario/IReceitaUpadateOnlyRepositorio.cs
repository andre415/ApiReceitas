namespace MeuLivroDeReceitas.Domain.Repositorio.Usuario;
public interface IReceitaUpadateOnlyRepositorio
{
     void Update (Entidades.Receita receita);
    public Task<Entidades.Receita> RecuperarReceitaPorId(long ReceitaId1);


}

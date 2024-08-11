namespace MeuLivroDeReceitas.Domain.Entidades;

public class EntidadeBase 
{
    public long Id { get; set; }

    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
}

using MeuLivroDeReceitas.Domain.Enum;

namespace MeuLivroDeReceitas.Domain.Entidades
{
    public class Receita : EntidadeBase
    {
        public string Titulo { get; set; }
        public Categoria Categoria { get; set; }
        public string ModoDePreparo { get; set; }
        public int TempoDePreparo { get; set; }
        public ICollection<Ingredientes> ingredientes { get; set; }

        public long UsuarioId { get; set; }
    }
}

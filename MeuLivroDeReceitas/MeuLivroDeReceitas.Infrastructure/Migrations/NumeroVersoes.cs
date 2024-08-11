using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Infrastructure.Migrations
{
    public enum NumeroVersoes
    {
        CriarTabelaUsuario = 1,
        CriarTabelaDeReceitas = 2,
        AlterarTabelaDeReceitas = 3,
        CriarTabelaAssociacaoUsuario = 4, 
    }
}

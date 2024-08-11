using MeuLivroDeReceitas.Comunicacao.Respostas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Application.UseCases.Conexao.RecuperarTodasAsConexoesUseCase;
public interface IRecuperarTodasAsConexoesUseCase
{
    Task<IList<RespostaUsuarioConectadoJson>> Executar();
}

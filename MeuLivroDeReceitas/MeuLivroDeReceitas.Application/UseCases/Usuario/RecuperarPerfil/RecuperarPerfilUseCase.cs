using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Respostas;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.RecuperarPerfil;
public class RecuperarPerfilUseCase : IRecuperarPerfilUseCase
{
    private IMapper _mapper;
    private IUsuarioLogado _usuarioLogado;

    public RecuperarPerfilUseCase(IMapper mapper, IUsuarioLogado usuarioLogado)
    {
        _mapper = mapper;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<RespostaPerfilJson> Executar()
    {
        var usuario = await _usuarioLogado.RecuperarUsuario();
        return _mapper.Map<RespostaPerfilJson>(usuario);
    }
}

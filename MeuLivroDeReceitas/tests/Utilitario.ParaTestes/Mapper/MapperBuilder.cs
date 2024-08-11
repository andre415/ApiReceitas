using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.AutoMapper;

namespace Utilitario.ParaTestes.Mapper;

public class MapperBuilder
{
    public static IMapper Instancia()
    {
        var configuracao = new MapperConfiguration(cgf => {
            cgf.AddProfile<AutoMapperConfig>(); 
        });

        return configuracao.CreateMapper();
    }
}

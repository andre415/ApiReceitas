using System.Globalization;

namespace MeuLivroDeReceitas.Domain.Extensions;
public static class StringExtension
{
    public static bool CompararSemAcentosOuLetrasMaiusculas(this string origem, string pesquisarPor)
    {   
        var index = CultureInfo.CurrentCulture.CompareInfo.IndexOf(origem,pesquisarPor, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);

        return index >= 0;
    }
}

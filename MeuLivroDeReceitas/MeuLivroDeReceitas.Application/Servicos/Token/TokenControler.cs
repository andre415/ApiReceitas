using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeuLivroDeReceitas.Application.Servicos.Token;

public class TokenControler
{
    private const string EmailAlias = "eml";
    private readonly double _tempoDeVidaDoTokenEmMinutos;
    private readonly string _chaveDeSeguranca;

    public TokenControler(double tempoDeVidaDoTokenEmMinutos, string chaveDeSeguranca)
    {
        _tempoDeVidaDoTokenEmMinutos = tempoDeVidaDoTokenEmMinutos;
        _chaveDeSeguranca = chaveDeSeguranca;
    }
    
    public string RecuperarEmail(string tokem)
    {
        var clains = ValidarToken(tokem);
        return clains.FindFirst(EmailAlias).Value;
    }
    public string GerarToken(string emailDoUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(EmailAlias, emailDoUsuario),
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tempoDeVidaDoTokenEmMinutos),
            SigningCredentials = new SigningCredentials(SymmetricKey(),
            SecurityAlgorithms.HmacSha256Signature)    
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);

    }

    public ClaimsPrincipal ValidarToken (string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var parametrosDeValidacao = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SymmetricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        var clains = tokenHandler.ValidateToken(token, parametrosDeValidacao, out _);

        return clains;
    }
    private SymmetricSecurityKey SymmetricKey()
    {
        var symmetricKey = Convert.FromBase64String(_chaveDeSeguranca);
        return new SymmetricSecurityKey(symmetricKey);
    }
}

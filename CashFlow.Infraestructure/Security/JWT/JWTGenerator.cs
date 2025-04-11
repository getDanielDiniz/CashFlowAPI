using CashFlow.Domain.Entities;
using CashFlow.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashFlow.Infraestructure.Security.JWT;
internal class JWTGenerator : IJWTGenerator
{
    private string _secretKey;
    private readonly double _expiresInMinutes;

    public JWTGenerator(IConfiguration configuration)
    {
        _secretKey = configuration.GetRequiredSection("TokenConfigs:SignInKey").Value;
        string StrMinutesToExpire = configuration.GetRequiredSection("TokenConfigs:MinutesToExpire").Value;
        _expiresInMinutes = int.Parse(StrMinutesToExpire);
    }


    public string GenerateJWTToken(User user)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Sid,user.UserIdentifier.ToString()),
        ];

        SecurityTokenDescriptor descriptor = new()
        {
            Expires = DateTime.UtcNow.AddMinutes(_expiresInMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        }; 

        JwtSecurityTokenHandler handler = new();
        SecurityToken token = handler.CreateToken(descriptor);
        
        return handler.WriteToken(token);

    }
    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_secretKey);     
        return new SymmetricSecurityKey(key);
    }
}

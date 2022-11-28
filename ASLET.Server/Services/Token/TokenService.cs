using System.Security.Claims;
using System.Text;
using ASLET.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ASLET.Server.Services.Token;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    
    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:TokenKey").Value));
    }
    
    public string CreateToken(AsletUser user)
    { 
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Fullname()),
            new Claim(ClaimTypes.Role, "User")
        };

        var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(365),
            SigningCredentials = cred
        };

        var tokenHandler = new JsonWebTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return token;
    }
}
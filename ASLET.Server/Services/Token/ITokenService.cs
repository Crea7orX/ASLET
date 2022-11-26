using ASLET.Models;

namespace ASLET.Server.Services.Token;

public interface ITokenService
{
    string CreateToken(AsletUser currentUser);
}
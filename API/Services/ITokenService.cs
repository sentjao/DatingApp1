using API.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public interface ITokenService
    {
         string CreateToken(AppUser user);
         SymmetricSecurityKey Key{get;}
    }
}
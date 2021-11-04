using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace API.Utils
{
    public static class Extensions
    {
        public static string ToJwtTokenString(this SecurityTokenDescriptor tokenDescriptor)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}
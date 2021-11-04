using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Entities;
using API.Services;
using API.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.ServicesImpl
{
    public class TokenService : ITokenService
    {
        public SymmetricSecurityKey Key{get;}
        public TokenService(IConfiguration config, SymmetricSecurityKey key)
        {
            Key = key;
        }
        public string CreateToken(AppUser user)
        {
            return new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(7),
                Subject = GetSubject(user),
                SigningCredentials = new SigningCredentials(Key, 
                                        SecurityAlgorithms.HmacSha512Signature)
            }.ToJwtTokenString();
        }

        private ClaimsIdentity GetSubject(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)                
            };
            return new ClaimsIdentity(claims);
        }
    }
}
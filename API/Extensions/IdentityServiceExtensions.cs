using System;
using System.Text;
using API.ServicesImpl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options=>{
                      options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuerSigningKey= true,
                            IssuerSigningKey = services.BuildServiceProvider().GetService<SymmetricSecurityKey>(),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                  });
            services.AddSingleton<SymmetricSecurityKey>(x=>CreateSymmetricSecurityKey(x, config));
            return services;
        }

        private static SymmetricSecurityKey CreateSymmetricSecurityKey(IServiceProvider sp, IConfiguration config)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
    }
}
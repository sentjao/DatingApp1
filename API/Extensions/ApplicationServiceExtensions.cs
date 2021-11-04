using API.Data;
using API.DTO.Encryption;
using API.DTO.Results;
using API.Entities;
using API.Entities.Requests.DTO;
using API.Mapping;
using API.Services;
using API.ServicesImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddSingleton<IEncryptionService<HMACSHA512EncryptionResult>, HMACSHA512EncryptionService>()
                .AddScoped<ITokenService, TokenService>()
                .AddDbContext<DataContext>(x =>
                    {
                        x.UseSqlite(config.GetConnectionString("DefaultConnection"));
                    })
                .AddMapping();
        }

           private static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddScoped<IMapper<AppUser, RegistrationRequest, AppUserResult>, 
                AppUserAppUserResultMapper>();
            return services;
        }

    }
}
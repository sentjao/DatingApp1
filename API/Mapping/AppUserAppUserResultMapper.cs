using API.DTO.Encryption;
using API.DTO.Results;
using API.Entities;
using API.Entities.Requests.DTO;
using API.Services;

namespace API.Mapping
{
    public class AppUserAppUserResultMapper : EntityDtoMapperBase<AppUser, RegistrationRequest, AppUserResult>,
        IMapper<AppUser, RegistrationRequest, AppUserResult>
    {
        private readonly IEncryptionService<HMACSHA512EncryptionResult> _encryptionService;
         private readonly ITokenService _tokenService;
        public AppUserAppUserResultMapper(IEncryptionService<HMACSHA512EncryptionResult> encryptionService,
                        ITokenService tokenService)
        {
            _tokenService = tokenService;
            _encryptionService = encryptionService;
        }
    public override AppUser DtoToEntity(RegistrationRequest dto)
    {
        var encryptionResult = _encryptionService.EncryptAsync(dto.Password).Result;
        return new AppUser
        {
            UserName = dto.UserName,
            PasswordHash = encryptionResult.EncryptedText,
            PasswordSalt = encryptionResult.Salt
        };
    }

    public override AppUserResult EntityToDto(AppUser entity)
    {
        return new AppUserResult
        {
            UserName = entity.UserName,
            Token = _tokenService.CreateToken(entity)
        };
    }
}
}
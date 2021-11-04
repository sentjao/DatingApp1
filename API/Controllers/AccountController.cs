using System.Threading.Tasks;
using API.Data;
using API.DTO.Results;
using API.Entities;
using API.DTO.Encryption;
using API.Entities.Requests.DTO;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Mapping;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly DataContext _context;
        private readonly IEncryptionService<HMACSHA512EncryptionResult> _encryptionService;
        private readonly ITokenService _tokenService;
        private readonly IMapper<AppUser, RegistrationRequest, AppUserResult> _mapper;

        public AccountController(DataContext context,
        IEncryptionService<HMACSHA512EncryptionResult> encryptionService,
        ITokenService tokenService,
        IMapper<AppUser, RegistrationRequest, AppUserResult> mapper)
        {
            _tokenService = tokenService;
            _encryptionService = encryptionService;
            _context = context;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AppUserResult>> Register(RegistrationRequest registrationRequest)
        {
            if (!await ValidateUserNameAsync(registrationRequest.UserName))
            {
                return BadRequest("UserName is not unique");
            }
            var user = _mapper.DtoToEntity(registrationRequest);
            await SaveUserAsync(user);
            return _mapper.EntityToDto(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AppUserResult>> Login(LoginRequest request)
        {
            var user = await FindByUserNameAsync(request.UserName);
            if (user == null)
            {
                return Unauthorized("User name is not found");
            }
            if (!await ValidatePasswordAsync(request.Password, user))
            {
                return Unauthorized("Wrong Password");
            }
            return _mapper.EntityToDto(user);
        }

        private async Task<bool> ValidatePasswordAsync(string password, AppUser user)
        {
            return await _encryptionService.ValidateAsync(password,
                new HMACSHA512EncryptionResult
                {
                    EncryptedText = user.PasswordHash,
                    Salt = user.PasswordSalt
                });
        }

        private async Task<AppUser> FindByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
        }

        private async Task<bool> ValidateUserNameAsync(string userName)
        {
            return !await _context.Users.AnyAsync(x => x.UserName.ToLower().Trim() == userName.ToLower().Trim());
        }

        private async Task SaveUserAsync(AppUser user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
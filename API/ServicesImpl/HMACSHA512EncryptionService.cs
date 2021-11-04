using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTO.Encryption;
using API.Services;

namespace API.ServicesImpl
{
    public class HMACSHA512EncryptionService : IEncryptionService<HMACSHA512EncryptionResult>
    {
        public async Task<HMACSHA512EncryptionResult> EncryptAsync(string plainValue)
        {
            using(var hmac = await EncryptPasswordAsync(plainValue))
            {
                return new HMACSHA512EncryptionResult
                {
                    EncryptedText = hmac.Hash,
                    Salt = hmac.Key
                };
            }
        }

        public async Task<bool> ValidateAsync(string plainValue, HMACSHA512EncryptionResult encryptionResult)
        {
            using(var hmac = new HMACSHA512(encryptionResult.Salt))
            {
                  using(var ms = new MemoryStream(Encoding.UTF8.GetBytes(plainValue)))
                  {
                     var pwdBytes = await hmac.ComputeHashAsync(ms);
                     return pwdBytes.SequenceEqual(encryptionResult.EncryptedText);
                  }  
            }
        }

        private async Task<HMACSHA512> EncryptPasswordAsync(string password)
        {
            using(var ms = new MemoryStream(Encoding.UTF8.GetBytes(password)))
            {
                var hmac = new HMACSHA512();
                var pwdBytes = await hmac.ComputeHashAsync(ms);
                return hmac;
            }
        }
    }
}
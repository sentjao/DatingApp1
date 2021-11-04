using System.Threading.Tasks;
using API.DTO.Encryption;

namespace API.Services
{
    public interface IEncryptionService<TResult> where TResult: EncryptionResult 
    {
         Task<TResult> EncryptAsync(string plainValue);

         Task<bool> ValidateAsync(string plainValue, TResult encryptionResult);
    }
}
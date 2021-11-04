namespace API.DTO.Encryption
{
    public class HMACSHA512EncryptionResult: EncryptionResult
    {
        public byte[] Salt{get; set;}
    }
}
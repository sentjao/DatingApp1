namespace API.Entities
{
    public class AppUser: EntityBase
    {
        public string UserName { get; set; }
        public byte[] PasswordHash{get; set;}
        public byte[] PasswordSalt{get; set;}
    }
}
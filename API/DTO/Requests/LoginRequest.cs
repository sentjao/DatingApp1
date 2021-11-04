using System.ComponentModel.DataAnnotations;
using API.DTO;
using Newtonsoft.Json;

namespace API.Entities.Requests.DTO
{
    public class LoginRequest: DtoBase
    {
        [Required]
        [JsonRequired]
        [JsonProperty("username")]
        public string UserName{get; set;}
        
        [Required]
        [JsonRequired]
        [JsonProperty("password")]
        public string Password{get; set;}
    }
}
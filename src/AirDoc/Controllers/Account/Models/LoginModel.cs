using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AirDoc.Controllers.Account.Models
{
    public class LoginModel
    {
        [JsonProperty("userName")]
        [Required]
        public string UserName { get; set; }

        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }
        
        [JsonProperty("rememberMe")]
        public bool RememberMe { get; set; }
    }
}

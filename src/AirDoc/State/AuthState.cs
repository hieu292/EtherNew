using Newtonsoft.Json;
using AirDoc.Models.Api;

namespace AirDoc.State
{
    public class AuthState
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("loggedIn")]
        public bool LoggedIn { get; set; }
    }
}

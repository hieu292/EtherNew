using Newtonsoft.Json;

namespace AirDoc.Controllers.Manage.Models
{
    public class RemoveExternalLoginModel
    {
        [JsonProperty("loginProvider")]
        public string LoginProvider { get; set; }

        [JsonProperty("providerKey")]
        public string ProviderKey { get; set; }
    }
}

using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V1
{
    public class FirewallConfig : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("config")]
        public string Config { get; set; }
    }
}
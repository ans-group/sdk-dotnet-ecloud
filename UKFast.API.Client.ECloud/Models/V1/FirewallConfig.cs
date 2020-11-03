using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models.V1
{
    public class FirewallConfig : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("config")]
        public string Config { get; set; }
    }
}
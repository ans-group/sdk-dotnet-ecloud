using Newtonsoft.Json;

namespace ANS.API.Client.ECloud.Models.V2.Request
{
    public class CreateNetworkRequest
    {
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("router_id")]
        public string RouterID { get; set; }
    }
}
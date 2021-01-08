using Newtonsoft.Json;

namespace UKFast.API.Client.ECloud.Models.V2.Request
{
    public class CreateFirewallPolicyRequest
    {
        [JsonProperty("router_id")]
        public string RouterID { get; set; }

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("sequence")]
        public int Sequence { get; set; }
    }
}
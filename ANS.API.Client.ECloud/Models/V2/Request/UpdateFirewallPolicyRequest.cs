using Newtonsoft.Json;

namespace ANS.API.Client.ECloud.Models.V2.Request
{
    public class UpdateFirewallPolicyRequest
    {
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("sequence", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Sequence { get; set; }
    }
}
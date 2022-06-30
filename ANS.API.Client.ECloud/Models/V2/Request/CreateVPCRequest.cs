using Newtonsoft.Json;

namespace ANS.API.Client.ECloud.Models.V2.Request
{
    public class CreateVPCRequest
    {
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("region_id")]
        public string RegionID { get; set; }
    }
}
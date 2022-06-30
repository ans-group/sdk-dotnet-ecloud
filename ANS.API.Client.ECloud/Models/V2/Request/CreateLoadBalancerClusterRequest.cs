using Newtonsoft.Json;

namespace ANS.API.Client.ECloud.Models.V2.Request
{
    public class CreateLoadBalancerClusterRequest
    {
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("vpc_id")]
        public string VPCID { get; set; }

        [JsonProperty("availability_zone_id")]
        public string AvailabilityZoneID { get; set; }

        [JsonProperty("nodes")]
        public int Nodes { get; set; }
    }
}
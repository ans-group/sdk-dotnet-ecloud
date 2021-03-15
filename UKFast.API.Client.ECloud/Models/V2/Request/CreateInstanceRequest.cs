using Newtonsoft.Json;
using System.Collections.Generic;

namespace UKFast.API.Client.ECloud.Models.V2.Request
{
    public class CreateInstanceRequest
    {
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("vpc_id")]
        public string VPCID { get; set; }

        [JsonProperty("image_id")]
        public string ImageID { get; set; }

        [JsonProperty("image_data", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, object> ImageData { get; set; }

        [JsonProperty("vcpu_cores")]
        public int VCPUCores { get; set; }

        [JsonProperty("ram_capacity")]
        public int RAMCapacity { get; set; }

        [JsonProperty("availability_zone_id")]
        public string AvailabilityZoneID { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("volume_capacity")]
        public int VolumeCapacity { get; set; }

        [JsonProperty("network_id")]
        public string NetworkID { get; set; }

        [JsonProperty("floating_ip_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FloatingIPID { get; set; }

        [JsonProperty("requires_floating_ip")]
        public bool RequiresFloatingIP { get; set; }

        [JsonProperty("user_script", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string UserScript { get; set; }
    }
}
using Newtonsoft.Json;
using System;
using ANS.API.Client.Json;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V2
{
    public class Instance : ModelBase
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("vpc_id")]
        public string VPCID { get; set; }

        [JsonProperty("availability_zone_id")]
        public string AvailabilityZoneID { get; set; }

        [JsonProperty("image_id")]
        public string ImageID { get; set; }

        [JsonProperty("vcpu_cores")]
        public int VCPUCores { get; set; }

        [JsonProperty("ram_capacity")]
        public int RAMCapacity { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("volume_capacity")]
        public int VolumeCapacity { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("agent_running")]
        public bool AgentRunning { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}
using Newtonsoft.Json;
using System;
using ANS.API.Client.Json;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V2
{
    public class Volume : ModelBase
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("vpc_id")]
        public string VPCID { get; set; }

        [JsonProperty("availability_zone_id")]
        public string AvailabilityZoneID { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}
using Newtonsoft.Json;
using System;
using UKFast.API.Client.Json;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models.V2
{
    public class VPC : ModelBase
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("region_id")]
        public string RegionID { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}
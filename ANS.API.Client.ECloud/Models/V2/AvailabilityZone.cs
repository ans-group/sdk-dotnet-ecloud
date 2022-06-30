using Newtonsoft.Json;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V2
{
    public class AvailabilityZone : ModelBase
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("datacentre_site_id")]
        public int DatacentreSiteID { get; set; }
    }
}
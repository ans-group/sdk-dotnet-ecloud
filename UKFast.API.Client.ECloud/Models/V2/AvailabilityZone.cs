using Newtonsoft.Json;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models.V2
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
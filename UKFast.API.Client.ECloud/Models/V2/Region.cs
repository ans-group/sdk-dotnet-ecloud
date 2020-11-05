using Newtonsoft.Json;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models.V2
{
    public class Region : ModelBase
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
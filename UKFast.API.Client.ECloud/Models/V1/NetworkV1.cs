using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models.V1
{
    public class NetworkV1 : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int ID { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }
    }
}
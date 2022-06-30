using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V1
{
    public class Solution : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int ID { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("environment")]
        public string Environment { get; set; }

        [Newtonsoft.Json.JsonProperty("pod_id")]
        public int PodID { get; set; }
    }
}
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models.V1
{
    public class Site : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int ID { get; set; }

        [Newtonsoft.Json.JsonProperty("state")]
        public string State { get; set; }

        [Newtonsoft.Json.JsonProperty("solution_id")]
        public int SolutionID { get; set; }

        [Newtonsoft.Json.JsonProperty("pod_id")]
        public int PodID { get; set; }
    }
}
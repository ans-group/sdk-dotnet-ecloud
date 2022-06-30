using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V1
{
    public class Credit : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("type")]
        public string Type { get; set; }

        [Newtonsoft.Json.JsonProperty("total")]
        public int Total { get; set; }

        [Newtonsoft.Json.JsonProperty("remaining")]
        public int Remaining { get; set; }
    }
}
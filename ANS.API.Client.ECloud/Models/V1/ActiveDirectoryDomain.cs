using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V1
{
    public class ActiveDirectoryDomain : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int ID { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }
    }
}
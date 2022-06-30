using Newtonsoft.Json;

namespace ANS.API.Client.ECloud.Models.V2.Request
{
    public class UpdateInstanceRequest
    {
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }
    }
}
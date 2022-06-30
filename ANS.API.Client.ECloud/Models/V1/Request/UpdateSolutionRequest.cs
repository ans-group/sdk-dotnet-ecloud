using Newtonsoft.Json;

namespace ANS.API.Client.ECloud.Models.V1.Request
{
    public class UpdateSolutionRequest
    {
        [Newtonsoft.Json.JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}
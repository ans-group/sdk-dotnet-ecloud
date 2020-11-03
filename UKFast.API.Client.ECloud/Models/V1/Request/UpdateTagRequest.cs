using Newtonsoft.Json;

namespace UKFast.API.Client.ECloud.Models.V1.Request
{
    public class UpdateTagRequest
    {
        [Newtonsoft.Json.JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }
}
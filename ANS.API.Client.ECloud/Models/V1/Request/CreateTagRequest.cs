namespace ANS.API.Client.ECloud.Models.V1.Request
{
    public class CreateTagRequest
    {
        [Newtonsoft.Json.JsonProperty("key")]
        public string Key { get; set; }

        [Newtonsoft.Json.JsonProperty("value")]
        public string Value { get; set; }
    }
}
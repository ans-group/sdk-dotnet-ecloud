using Newtonsoft.Json;

namespace ANS.API.Client.ECloud.Models.V2.Request
{
    public class CreateVPNRequest
    {
        [JsonProperty("router_id")]
        public string RouterID { get; set; }
    }
}
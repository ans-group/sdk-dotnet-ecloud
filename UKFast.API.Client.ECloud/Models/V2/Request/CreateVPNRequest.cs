using Newtonsoft.Json;

namespace UKFast.API.Client.ECloud.Models.V2.Request
{
    public class CreateVPNRequest
    {
        [JsonProperty("router_id")]
        public string RouterID { get; set; }
    }
}
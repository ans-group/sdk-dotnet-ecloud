using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V1
{
    public enum FirewallRole
    {
        [System.Runtime.Serialization.EnumMember(Value = "Single")]
        Single,

        [System.Runtime.Serialization.EnumMember(Value = "Master")]
        Master,

        [System.Runtime.Serialization.EnumMember(Value = "Slave")]
        Slave
    }

    public class Firewall : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int ID { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("hostname")]
        public string Hostname { get; set; }

        [Newtonsoft.Json.JsonProperty("ip")]
        public string IP { get; set; }

        [Newtonsoft.Json.JsonProperty("role")]
        public FirewallRole Role { get; set; }
    }
}
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V1
{
    public enum ParameterType
    {
        [System.Runtime.Serialization.EnumMember(Value = "String")]
        String,

        [System.Runtime.Serialization.EnumMember(Value = "Numeric")]
        Numeric,

        [System.Runtime.Serialization.EnumMember(Value = "Boolean")]
        Boolean,

        [System.Runtime.Serialization.EnumMember(Value = "Password")]
        Password,
    }

    public class ApplianceParameter : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string ID { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("key")]
        public string Key { get; set; }

        [Newtonsoft.Json.JsonProperty("type")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ParameterType Type { get; set; }

        [Newtonsoft.Json.JsonProperty("description")]
        public string Description { get; set; }

        [Newtonsoft.Json.JsonProperty("required")]
        public bool Required { get; set; }

        [Newtonsoft.Json.JsonProperty("validation_rule")]
        public string ValidationRule { get; set; }
    }
}
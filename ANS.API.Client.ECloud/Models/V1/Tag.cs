using System;
using ANS.API.Client.Json;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V1
{
    public class Tag : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("key")]
        public string Key { get; set; }

        [Newtonsoft.Json.JsonProperty("value")]
        public string Value { get; set; }

        [Newtonsoft.Json.JsonProperty("created_at")]
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedAt { get; set; }
    }
}
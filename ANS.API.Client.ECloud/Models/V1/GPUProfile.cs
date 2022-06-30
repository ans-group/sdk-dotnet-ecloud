using System;
using ANS.API.Client.Json;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V1
{
    public class GPUProfile : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string ID { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("profile_name")]
        public string ProfileName { get; set; }

        [Newtonsoft.Json.JsonProperty("card_type")]
        public string CardType { get; set; }

        [Newtonsoft.Json.JsonProperty("created_at")]
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedAt { get; set; }
    }
}
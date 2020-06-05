using System;
using System.Collections.Generic;
using System.Text;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models
{
    public class Pod : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int ID { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("services")]
        public PodServices Services { get; set; }
    }

    public class PodServices
    {
        [Newtonsoft.Json.JsonProperty("public")]
        public bool Public { get; set; }

        [Newtonsoft.Json.JsonProperty("burst")]
        public bool Burst { get; set; }

        [Newtonsoft.Json.JsonProperty("appliances")]
        public bool Appliances { get; set; }

        [Newtonsoft.Json.JsonProperty("gpu")]
        public bool GPU { get; set; }
    }
}

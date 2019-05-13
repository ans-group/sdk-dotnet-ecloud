using System;
using System.Collections.Generic;
using System.Text;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models
{
    public class Solution : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int ID { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("environment")]
        public string Environment { get; set; }

        [Newtonsoft.Json.JsonProperty("pod_id")]
        public int PodID { get; set; }
    }
}

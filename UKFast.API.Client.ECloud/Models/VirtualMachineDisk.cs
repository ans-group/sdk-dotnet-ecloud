using System;
using System.Collections.Generic;
using System.Text;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models
{
    public class VirtualMachineDisk : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("uuid")]
        public string UUID { get; set; }

        [Newtonsoft.Json.JsonProperty("type")]
        public string Type { get; set; }

        [Newtonsoft.Json.JsonProperty("key")]
        public int Key { get; set; }

		[Newtonsoft.Json.JsonProperty("capacity")]
        public int Capacity { get; set; }
    }
}

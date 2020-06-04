using System;
using System.Collections.Generic;
using System.Text;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models
{
    public class Host : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int ID { get; set; }

        [Newtonsoft.Json.JsonProperty("solution_id")]
        public int SolutionID { get; set; }

        [Newtonsoft.Json.JsonProperty("pod_id")]
        public int PodID { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("cpu")]
        public HostCPU CPU { get; set; }

        [Newtonsoft.Json.JsonProperty("ram")]
        public HostRAM RAM { get; set; }
    }

    public class HostCPU
    {
        [Newtonsoft.Json.JsonProperty("qty")]
        public int Quantity { get; set; }

        [Newtonsoft.Json.JsonProperty("cores")]
        public int Cores { get; set; }

        [Newtonsoft.Json.JsonProperty("speed")]
        public string Speed { get; set; }
    }

    public class HostRAM
    {
        [Newtonsoft.Json.JsonProperty("capacity")]
        public int Capacity { get; set; }

        [Newtonsoft.Json.JsonProperty("reserved")]
        public int Reserved { get; set; }

        [Newtonsoft.Json.JsonProperty("allocated")]
        public int Allocated { get; set; }

        [Newtonsoft.Json.JsonProperty("available")]
        public int Available { get; set; }
    }
}

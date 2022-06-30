using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Models.V1
{
    public class Template : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("cpu")]
        public int CPU { get; set; }

        [Newtonsoft.Json.JsonProperty("ram")]
        public int RAM { get; set; }

        [Newtonsoft.Json.JsonProperty("hdd")]
        public int HDD { get; set; }

        [Newtonsoft.Json.JsonProperty("hdd_disks")]
        public VirtualMachineDisk[] HDDDisks { get; set; }

        [Newtonsoft.Json.JsonProperty("platform")]
        public string Platform { get; set; }

        [Newtonsoft.Json.JsonProperty("operating_system")]
        public string OperatingSystem { get; set; }

        [Newtonsoft.Json.JsonProperty("solution_id")]
        public string SolutionID { get; set; }
    }
}
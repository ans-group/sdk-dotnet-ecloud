using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models.V1
{
    public class VirtualMachine : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int ID { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("hostname")]
        public string Hostname { get; set; }

        [Newtonsoft.Json.JsonProperty("computername")]
        public string ComputerName { get; set; }

        [Newtonsoft.Json.JsonProperty("cpu")]
        public int CPU { get; set; }

        [Newtonsoft.Json.JsonProperty("ram")]
        public int RAM { get; set; }

        [Newtonsoft.Json.JsonProperty("hdd")]
        public int HDD { get; set; }

        [Newtonsoft.Json.JsonProperty("ip_internal")]
        public string IPInternal { get; set; }

        [Newtonsoft.Json.JsonProperty("ip_external")]
        public string IPExternal { get; set; }

        [Newtonsoft.Json.JsonProperty("template")]
        public string Template { get; set; }

        [Newtonsoft.Json.JsonProperty("platform")]
        public string Platform { get; set; }

        [Newtonsoft.Json.JsonProperty("backup")]
        public bool Backup { get; set; }

        [Newtonsoft.Json.JsonProperty("support")]
        public bool Support { get; set; }

        [Newtonsoft.Json.JsonProperty("environment")]
        public string Environment { get; set; }

        [Newtonsoft.Json.JsonProperty("solution_id")]
        public int SolutionID { get; set; }

        [Newtonsoft.Json.JsonProperty("status")]
        public string Status { get; set; }

        [Newtonsoft.Json.JsonProperty("power_status")]
        public string PowerStatus { get; set; }

        [Newtonsoft.Json.JsonProperty("tools_status")]
        public string ToolsStatus { get; set; }

        [Newtonsoft.Json.JsonProperty("hdd_disks")]
        public VirtualMachineDisk[] HDDDisks { get; set; }

        [Newtonsoft.Json.JsonProperty("encrypted")]
        public bool Encrypted { get; set; }

        [Newtonsoft.Json.JsonProperty("role")]
        public string Role { get; set; }
    }
}
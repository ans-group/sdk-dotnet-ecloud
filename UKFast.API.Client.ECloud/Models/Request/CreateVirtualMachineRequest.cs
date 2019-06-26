using System;
using System.Collections.Generic;
using System.Text;

namespace UKFast.API.Client.ECloud.Models.Request
{
    public class CreateVirtualMachineRequest
    {
        [Newtonsoft.Json.JsonProperty("environment")]
        public string Environment { get; set; }

        [Newtonsoft.Json.JsonProperty("template")]
        public string Template { get; set; }

        [Newtonsoft.Json.JsonProperty("template_password")]
        public string TemplatePassword { get; set; }

        [Newtonsoft.Json.JsonProperty("appliance_id")]
        public string ApplianceID { get; set; }

        [Newtonsoft.Json.JsonProperty("role")]
        public string Role { get; set; }

        [Newtonsoft.Json.JsonProperty("parameters")]
        public IEnumerable<CreateVirtualMachineRequestParameter> Parameters { get; set; }

        [Newtonsoft.Json.JsonProperty("cpu")]
        public int cpu { get; set; }

        [Newtonsoft.Json.JsonProperty("ram")]
        public int RAM { get; set; }

        [Newtonsoft.Json.JsonProperty("hdd")]
        public int HDD { get; set; }

        [Newtonsoft.Json.JsonProperty("hdd_disks")]
        public IEnumerable<CreateVirtualMachineRequestDisk> HDDDisks { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("computername")]
        public string ComputerName { get; set; }

        [Newtonsoft.Json.JsonProperty("tags")]
        public IEnumerable<CreateTagRequest> Tags { get; set; }

        [Newtonsoft.Json.JsonProperty("backup")]
        public bool Backup { get; set; }

        [Newtonsoft.Json.JsonProperty("support")]
        public bool Support { get; set; }

        [Newtonsoft.Json.JsonProperty("monitoring")]
        public bool Monitoring { get; set; }

        [Newtonsoft.Json.JsonProperty("monitoring_contacts")]
        public IEnumerable<int> MonitoringContacts { get; set; }

        [Newtonsoft.Json.JsonProperty("solution_id")]
        public int SolutionID { get; set; }

        [Newtonsoft.Json.JsonProperty("datastore_id")]
        public int DatastoreID { get; set; }

        [Newtonsoft.Json.JsonProperty("site_id")]
        public int SiteID { get; set; }

        [Newtonsoft.Json.JsonProperty("network_id")]
        public int NetworkID { get; set; }

        [Newtonsoft.Json.JsonProperty("external_ip_required")]
        public bool ExternalIPRequired { get; set; }

        [Newtonsoft.Json.JsonProperty("ssh_keys")]
        public IEnumerable<string> SSHKeys { get; set; }
    }

    public class CreateVirtualMachineRequestParameter
    {
        [Newtonsoft.Json.JsonProperty("key")]
        public string Key { get; set; }

        [Newtonsoft.Json.JsonProperty("value")]
        public string Value { get; set; }
    }

    public class CreateVirtualMachineRequestDisk
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("capacity")]
        public int Capacity { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UKFast.API.Client.ECloud.Models.Request
{
    public class CreateVirtualMachineRequest
    {
        [Newtonsoft.Json.JsonProperty("environment")]
        public string Environment { get; set; }

        [Newtonsoft.Json.JsonProperty("template", NullValueHandling = NullValueHandling.Ignore)]
        public string Template { get; set; }

        [Newtonsoft.Json.JsonProperty("template_password", NullValueHandling = NullValueHandling.Ignore)]
        public string TemplatePassword { get; set; }

        [Newtonsoft.Json.JsonProperty("appliance_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ApplianceID { get; set; }

        [Newtonsoft.Json.JsonProperty("role")]
        public string Role { get; set; }

        [Newtonsoft.Json.JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CreateVirtualMachineRequestParameter> Parameters { get; set; }

        [Newtonsoft.Json.JsonProperty("cpu")]
        public int cpu { get; set; }

        [Newtonsoft.Json.JsonProperty("ram")]
        public int RAM { get; set; }

        [Newtonsoft.Json.JsonProperty("hdd", NullValueHandling = NullValueHandling.Ignore)]
        public int? HDD { get; set; }

        [Newtonsoft.Json.JsonProperty("hdd_disks", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CreateVirtualMachineRequestDisk> HDDDisks { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("computername", NullValueHandling = NullValueHandling.Ignore)]
        public string ComputerName { get; set; }

        [Newtonsoft.Json.JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CreateTagRequest> Tags { get; set; }

        [Newtonsoft.Json.JsonProperty("backup", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Backup { get; set; }

        [Newtonsoft.Json.JsonProperty("support", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Support { get; set; }

        [Newtonsoft.Json.JsonProperty("monitoring", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Monitoring { get; set; }

        [Newtonsoft.Json.JsonProperty("monitoring_contacts", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<int> MonitoringContacts { get; set; }

        [Newtonsoft.Json.JsonProperty("solution_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SolutionID { get; set; } = null;

        [Newtonsoft.Json.JsonProperty("datastore_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? DatastoreID { get; set; } = null;

        [Newtonsoft.Json.JsonProperty("site_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SiteID { get; set; } = null;

        [Newtonsoft.Json.JsonProperty("network_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? NetworkID { get; set; } = null;

        [Newtonsoft.Json.JsonProperty("external_ip_required", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ExternalIPRequired { get; set; }

        [Newtonsoft.Json.JsonProperty("ssh_keys", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> SSHKeys { get; set; }

        [Newtonsoft.Json.JsonProperty("encrypt", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Encrypt { get; set; }
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

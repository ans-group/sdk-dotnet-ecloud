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
        public string Template { get; set; } = "";

        [Newtonsoft.Json.JsonProperty("template_password", NullValueHandling = NullValueHandling.Ignore)]
        public string TemplatePassword { get; set; } = "";

        [Newtonsoft.Json.JsonProperty("appliance_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ApplianceID { get; set; } = "";

        [Newtonsoft.Json.JsonProperty("role")]
        public string Role { get; set; }

        [Newtonsoft.Json.JsonProperty("parameters", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<CreateVirtualMachineRequestParameter> Parameters { get; set; } = null;

        [Newtonsoft.Json.JsonProperty("cpu")]
        public int cpu { get; set; }

        [Newtonsoft.Json.JsonProperty("ram")]
        public int RAM { get; set; }

        [Newtonsoft.Json.JsonProperty("hdd", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? HDD { get; set; }

        [Newtonsoft.Json.JsonProperty("hdd_disks", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<CreateVirtualMachineRequestDisk> HDDDisks { get; set; } = null;

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("computername", NullValueHandling = NullValueHandling.Ignore)]
        public string ComputerName { get; set; } = null;

        [Newtonsoft.Json.JsonProperty("tags", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<CreateTagRequest> Tags { get; set; } = null;

        [Newtonsoft.Json.JsonProperty("backup", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Backup { get; set; }

        [Newtonsoft.Json.JsonProperty("support", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Support { get; set; }

        [Newtonsoft.Json.JsonProperty("monitoring", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Monitoring { get; set; }

        [Newtonsoft.Json.JsonProperty("monitoring_contacts", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<int> MonitoringContacts { get; set; } = null;

        [Newtonsoft.Json.JsonProperty("solution_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? SolutionID { get; set; }

        [Newtonsoft.Json.JsonProperty("datastore_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? DatastoreID { get; set; }

        [Newtonsoft.Json.JsonProperty("site_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? SiteID { get; set; }

        [Newtonsoft.Json.JsonProperty("network_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? NetworkID { get; set; }

        [Newtonsoft.Json.JsonProperty("external_ip_required", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? ExternalIPRequired { get; set; }

        [Newtonsoft.Json.JsonProperty("ssh_keys", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> SSHKeys { get; set; }

        [Newtonsoft.Json.JsonProperty("encrypt", DefaultValueHandling = DefaultValueHandling.Ignore)]
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

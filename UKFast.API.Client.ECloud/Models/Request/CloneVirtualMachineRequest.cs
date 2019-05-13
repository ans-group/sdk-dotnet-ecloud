using System;
using System.Collections.Generic;
using System.Text;

namespace UKFast.API.Client.ECloud.Models.Request
{
    public class CloneVirtualMachineRequest
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }
    }
}

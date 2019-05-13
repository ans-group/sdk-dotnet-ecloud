using System;
using System.Collections.Generic;
using System.Text;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models
{
    public class FirewallConfig : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("config")]
        public string Config { get; set; }
    }
}

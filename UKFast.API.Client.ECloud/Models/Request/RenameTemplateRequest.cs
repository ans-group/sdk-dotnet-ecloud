using System;
using System.Collections.Generic;
using System.Text;

namespace UKFast.API.Client.ECloud.Models.Request
{
    public class RenameTemplateRequest
    {
        [Newtonsoft.Json.JsonProperty("destination")]
        public string Destination { get; set; }
    }
}

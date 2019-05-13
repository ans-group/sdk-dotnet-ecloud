using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UKFast.API.Client.ECloud.Models.Request
{
    public class UpdateTagRequest
    {
        [Newtonsoft.Json.JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }
}

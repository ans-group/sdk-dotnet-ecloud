using System;
using System.Collections.Generic;
using System.Text;

namespace UKFast.API.Client.ECloud.Models.Request
{
    public class CreateTagRequest
    {
        [Newtonsoft.Json.JsonProperty("key")]
        public string Key { get; set; }

        [Newtonsoft.Json.JsonProperty("value")]
        public string Value { get; set; }
    }
}

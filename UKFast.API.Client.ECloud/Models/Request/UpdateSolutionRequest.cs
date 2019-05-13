using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UKFast.API.Client.ECloud.Models.Request
{
    public class UpdateSolutionRequest
    {
        [Newtonsoft.Json.JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}

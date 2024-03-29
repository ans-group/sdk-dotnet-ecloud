﻿using Newtonsoft.Json;

namespace ANS.API.Client.ECloud.Models.V2.Request
{
    public class UpdateVPNRequest
    {
        [JsonProperty("router_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RouterID { get; set; }
    }
}
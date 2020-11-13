using Newtonsoft.Json;

namespace UKFast.API.Client.ECloud.Models.V1.Request
{
    public enum UpdateVirtualMachineRequestDiskState
    {
        [System.Runtime.Serialization.EnumMember(Value = "present")]
        Present,

        [System.Runtime.Serialization.EnumMember(Value = "absent")]
        Absent
    }

    public class UpdateVirtualMachineRequest
    {
        [Newtonsoft.Json.JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("cpu", NullValueHandling = NullValueHandling.Ignore)]
        public int CPU { get; set; }

        [Newtonsoft.Json.JsonProperty("ram", NullValueHandling = NullValueHandling.Ignore)]
        public int RAM { get; set; }
    }

    public class UpdateVirtualMachineRequestDisk
    {
        [Newtonsoft.Json.JsonProperty("uuid", NullValueHandling = NullValueHandling.Ignore)]
        public string UUID { get; set; }

        [Newtonsoft.Json.JsonProperty("capacity", NullValueHandling = NullValueHandling.Ignore)]
        public int Capacity { get; set; }

        [Newtonsoft.Json.JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public UpdateVirtualMachineRequestDiskState? State { get; set; }
    }
}
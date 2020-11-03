using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Models.V1
{
    public enum DatastoreStatus
    {
        [System.Runtime.Serialization.EnumMember(Value = "Completed")]
        Completed,

        [System.Runtime.Serialization.EnumMember(Value = "Failed")]
        Failed,

        [System.Runtime.Serialization.EnumMember(Value = "Expanding")]
        Expanding,

        [System.Runtime.Serialization.EnumMember(Value = "Queued")]
        Queued
    }

    public class Datastore : ModelBase
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int ID { get; set; }

        [Newtonsoft.Json.JsonProperty("solution_id")]
        public int SolutionID { get; set; }

        [Newtonsoft.Json.JsonProperty("site_id")]
        public int SiteID { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("status")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DatastoreStatus Status { get; set; }

        [Newtonsoft.Json.JsonProperty("capacity")]
        public int Capacity { get; set; }

        [Newtonsoft.Json.JsonProperty("allocated")]
        public int? Allocated { get; set; }

        [Newtonsoft.Json.JsonProperty("available")]
        public int? Available { get; set; }
    }
}
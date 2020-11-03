namespace UKFast.API.Client.ECloud.Models.V1.Request
{
    public enum CreateVirtualMachineTemplateRequestType
    {
        [System.Runtime.Serialization.EnumMember(Value = "solution")]
        Solution,

        [System.Runtime.Serialization.EnumMember(Value = "pod")]
        Pod
    }

    public class CreateVirtualMachineTemplateRequest
    {
        [Newtonsoft.Json.JsonProperty("template_name")]
        public string TemplateName { get; set; }

        [Newtonsoft.Json.JsonProperty("template_type")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public CreateVirtualMachineTemplateRequestType TemplateType { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace hitachiv1.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoleClass
    {
        Common,
        Admin,
        Super
    }
}
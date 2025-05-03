using System.Text.Json.Serialization;

namespace Ticketing_system.Models.Payments;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentStateResponse
{
    Created,
    Successful,
    Failed
}

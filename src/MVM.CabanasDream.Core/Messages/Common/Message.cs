using System.Text.Json.Serialization;

namespace MVM.CabanasDream.Core.Messages.Common;

public abstract class Message
{
    [JsonIgnore]
    public Guid MessageId { get; init; } = new();
    
    [JsonIgnore]
    public DateTime MessageTimeStamp { get; init; } = DateTime.Now;
    
    [JsonIgnore]
    public string MessageType { get; protected set; }
}
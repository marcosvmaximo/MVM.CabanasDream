namespace MVM.CabanasDream.Core.Messages.Common;

public abstract class Message
{
    public Guid MessageId { get; init; } = new();
    public DateTime MessageTimeStamp { get; init; } = DateTime.Now;
    public string MessageType { get; protected set; }
}
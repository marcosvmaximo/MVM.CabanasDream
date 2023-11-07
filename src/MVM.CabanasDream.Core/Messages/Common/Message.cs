namespace MVM.CabanasDream.Core.Messages.Common;

public abstract class Message
{
    public Guid MessageId { get; init; }
    public DateTime MessageTimeStamp { get; init; }
    protected Message()
    {
        MessageId = new Guid();
        MessageTimeStamp = DateTime.Now;
    }
    
    public string MessageType { get; protected set; }
}
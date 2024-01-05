using System.Net;
using MediatR;
using MVM.CabanasDream.Core.Messages.Common;

namespace MVM.CabanasDream.Core.Messages;

public class DomainNotification : Message, INotification
{
    public DomainNotification(string property, string message, object attemptedValue)
    {
        Property = property;
        Message = message;
        AttemptedValue = attemptedValue;
    }

    public DomainNotification(string property, string message) : this(property, message, null){}
    
    public string Property { get; set; }
    public string Message { get; set; }
    public object AttemptedValue { get; set; }
    public string ErrorCode { get; set; }
    
    public override string ToString() {
        return Message;
    }
}
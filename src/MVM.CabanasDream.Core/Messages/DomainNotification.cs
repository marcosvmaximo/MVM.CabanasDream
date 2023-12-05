using System.Net;
using MVM.CabanasDream.Core.Messages.Common;

namespace MVM.CabanasDream.Core.Messages;

public class DomainNotification : Event
{
    public DomainNotification(string key, string message)
    {
        Key = key;
        Message = message;
        Version = 1;
    }
    
    public DomainNotification(string message)
    {
        Key = "";
        Message = message;
        Version = 1;
    }
    
    public DomainNotification(HttpStatusCode code, string message)
    {
        Key = nameof(code);
        Message = message;
        Version = 1;
    }
    
    public string Key { get; private set; }
    public string Message { get; private set; }
    public int Version { get; private set; }
}
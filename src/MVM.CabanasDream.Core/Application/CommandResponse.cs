using FluentValidation.Results;
using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Core.Application;

public class CommandResponse
{
    public bool Success => Errors.Count == 0;
    public object Data { get; private set; }
    public Dictionary<string, object> Errors { get; private set; }

    private CommandResponse()
    {
        Errors = new Dictionary<string, object>();
    }   

    public void AddError(Dictionary<string, object>? error)
    {
        if (error != null)
        {
            foreach (var kvp in error)
            {
                Errors.Add(kvp.Key, kvp.Value);
            }
        }
    }
    
    public void AddError(string key, object value)
    {
        Errors.Add(key, value);
    }

    public static CommandResponse CustomResponse(List<ValidationFailure>? validationResultErrors, object data = null)
    {
        if (validationResultErrors is null || !validationResultErrors.Any())
        {
            return Failure(null);
        }
        
        var errors = new Dictionary<string, object>();
            
        foreach (var error in validationResultErrors)
        {
            errors.Add(error.PropertyName, error.ErrorMessage);
        }

        return Failure(errors, data);  
    }
    
    public static CommandResponse CustomResponse(List<DomainNotification>? domainNotificationsErrors, object data = null)
    {
        if (domainNotificationsErrors is null || !domainNotificationsErrors.Any())
        {
            return Failure(null);
        }
        
        var errors = new Dictionary<string, object>();
            
        foreach (var error in domainNotificationsErrors)
        {
            errors.Add(error.Property, error.Message);
        }

        return Failure(errors, data);  
    }
    public static CommandResponse CustomResponse(object data = null)
    {
        return Ok(data);
    }
    
    private static CommandResponse Ok(object data)
    {
        return new CommandResponse
        {
            Data = data
        };
    }

    private static CommandResponse Failure(Dictionary<string, object>? errors, object data = null)
    {
        var result = new CommandResponse()
        {
            Data = data
        };

        result.AddError(errors);

        return result;
    }
}
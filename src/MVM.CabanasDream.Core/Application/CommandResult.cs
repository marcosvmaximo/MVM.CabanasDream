using FluentValidation.Results;

namespace MVM.CabanasDream.Core.Application;

public class CommandResult
{
    public bool Success { get; private set; }
    public object Data { get; private set; }
    public Dictionary<string, object> Errors { get; private set; }

    private CommandResult()
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

    public static CommandResult CustomResponse(List<ValidationFailure>? validationResultErrors)
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

        return Failure(errors);  
    }
    
    public static CommandResult CustomResponse(object data = null)
    {
        return Ok(data);
    }
    
    private static CommandResult Ok(object data)
    {
        return new CommandResult
        {
            Success = true,
            Data = data
        };
    }

    private static CommandResult Failure(Dictionary<string, object>? errors)
    {
        var result = new CommandResult
        {
            Success = false
        };

        result.AddError(errors);

        return result;
    }
}
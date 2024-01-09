using System.Net;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;

namespace MVM.CabanasDream.Core.Application;

public abstract class Handler<TCommand> : IRequestHandler<TCommand, CommandResponse>
    where TCommand : Command
{
    protected IMessageBus _messager;
    protected ValidationResult ValidationResult { get; private set; }
    
    protected Handler(IMessageBus messager)
    {
        _messager = messager;
    }
    
    public abstract Task<CommandResponse> Handle(TCommand request, CancellationToken cancellationToken);

    protected virtual bool ValidarComando<TValidation>(TCommand command)
        where TValidation : AbstractValidator<TCommand>, new()
    {
        ValidationResult = command.FastValidation<TCommand, TValidation>();

        // foreach (var error in ValidationResult.Errors)
        // {
        //     _messager.PublishNotification(new DomainNotification(error.PropertyName, error.ErrorMessage));
        // }

        return ValidationResult.IsValid;
    }
    
    protected CommandResponse ReturnResponse(object data = null)
    {
        if(!ValidationResult.IsValid)
            return CommandResponse.CustomResponse(ValidationResult.Errors);

        return CommandResponse.CustomResponse(data);
    }

    protected void AddError(ValidationFailure fail)
    {
        ValidationResult.Errors.Add(fail);
    } 
    
    protected void AddError(string propertyName, string errorMessage)
    {
        AddError(new ValidationFailure(propertyName, errorMessage));
    }
    
    protected void AddError(string propertyName, string errorMessage, HttpStatusCode code)
    {
        var fail = new ValidationFailure(propertyName, errorMessage);
        fail.ErrorCode = code.ToString();
        
        AddError(fail);
    }
}
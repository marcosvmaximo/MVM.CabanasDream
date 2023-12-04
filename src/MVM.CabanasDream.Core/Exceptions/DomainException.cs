using System.Diagnostics.CodeAnalysis;

namespace MVM.CabanasDream.Core.Exceptions;

public class DomainException : Exception
{
    public DomainException()
    {
    }

    public DomainException(string message)
        : base(message)
    {
    }

    public DomainException(string message, Exception inner)
        : base(message, inner)
    {
    }    
    
    public static void ThrowIfNull(string? argument)
    {
        Throw(argument, null);
    }

    [DoesNotReturn]
    internal static void Throw(string? argument, Exception? value) =>
        throw new DomainException(argument, value);
}
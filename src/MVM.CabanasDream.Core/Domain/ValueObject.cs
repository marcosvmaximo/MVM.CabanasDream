namespace MVM.CabanasDream.Core.Domain;

public abstract class ValueObject
{
    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public abstract void Validar();
};
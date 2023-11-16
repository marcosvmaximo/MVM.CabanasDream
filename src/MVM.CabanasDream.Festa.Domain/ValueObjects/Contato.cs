using MVM.CabanasDream.Core.Domain;

namespace MVM.CabanasDream.Festa.Domain.ValueObjects;

public record Contato(string Ddd, string Telefone, string Email) : ValueObject
{
    public override void Validar()
    {
        throw new NotImplementedException();
    }
};
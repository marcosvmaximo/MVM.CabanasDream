using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Validation;

namespace MVM.CabanasDream.Festas.Domain.ValueObjects;

public class Contato : ValueObject
{
    public Contato(string ddd, string telefone, string email)
    {
        Ddd = ddd;
        Telefone = telefone;
        Email = email;
        
        Validar();
    }

    public string Ddd { get; private set; }
    public string Telefone { get; private set;  }
    public string Email { get; private set; }
    
    public sealed override void Validar()
    {
        AssertionConcern.AssertArgumentNotEmpty(Ddd, "O DDD não pode ser vazio");
        AssertionConcern.AssertArgumentNotNull(Ddd, "O DDD não pode ser vazio");
        AssertionConcern.AssertArgumentLength(Ddd, 2,2, "O DDD deve conter 2 caracteres");

        AssertionConcern.AssertArgumentNotNull(Telefone, "O telefone não pode ser vazio");
        AssertionConcern.AssertArgumentNotEmpty(Telefone, "O telefone não pode ser vazio");
        AssertionConcern.AssertArgumentLength(Telefone, 8, 11, "O telefone deve ter entre 8 e 11 caracteres");

        AssertionConcern.AssertArgumentNotEmpty(Email, "O email não pode ser vazio");
        AssertionConcern.AssertArgumentNotNull(Email, "O email não pode ser vazio");
        AssertionConcern.AssertEmail(Email, "O email não é válido");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Ddd;
        yield return Telefone;
        yield return Email;
    }
};
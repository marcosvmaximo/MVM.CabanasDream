using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Festa.Domain.ValueObjects;

namespace MVM.CabanasDream.Festa.Domain.Entities;

public class Cliente : Entity
{
    public string Nome { get; private set; }
    public string Sobrenome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public Endereco Endereco { get; private set; }
    public Contato Contato { get; private set; }
    
    public override void Validar()
    {
        throw new NotImplementedException();
    }
}
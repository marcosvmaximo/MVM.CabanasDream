using MVM.CabanasDream.Core.Domain;

namespace MVM.CabanasDream.Festas.Domain.Entities.Common;

public abstract class Pessoa : Entity
{
    protected Pessoa(string nome, DateTime dataNascimento, string cpf, string rg)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Cpf = cpf;
        Rg = rg;
    }

    protected Pessoa(){}
    public string Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public string Cpf { get; private set; }
    public string Rg { get; private set; }
}
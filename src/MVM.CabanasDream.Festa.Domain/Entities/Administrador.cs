using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festa.Domain.Enum;

namespace MVM.CabanasDream.Festa.Domain.Entities;

public class Administrador : Entity
{
    private List<Festa> _festas = new();

    public Administrador(string nome, DateTime dataNascimento, string cpf, string rg, ENivelPermissao nivelPermissao)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Cpf = cpf;
        Rg = rg;
        NivelPermissao = nivelPermissao;
    }

    public string Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public string Cpf { get; private set; }
    public string Rg { get; private set; }
    public ENivelPermissao NivelPermissao { get; private set; }

    public IReadOnlyCollection<Festa> Festas => _festas;
    
    public override void Validar()
    {       
        // Nome
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome do cliente deve ser informado.");
        AssertionConcern.AssertArgumentLength(Nome, 0, 100, "O nome do cliente não deve conter mais que 100 caracteres.");

        // Cpf
        AssertionConcern.AssertArgumentNotEmpty(Cpf, "O CPF do cliente deve ser informado.");
        AssertionConcern.AssertArgumentLength(Cpf, 11, 11, "O CPF deve ter 11 dígitos.");

        // Rg
        AssertionConcern.AssertArgumentNotEmpty(Rg, "O RG do administrador deve ser informado.");
        AssertionConcern.AssertArgumentLength(Rg, 9, 12, "O RG deve ter entre 9 e 12 dígitos.");

        // Data Nascimento
        AssertionConcern.AssertArgumentRange(DataNascimento, new DateTime(1900, 1, 1), DateTime.Now,
            "A data de nascimento deve ser maior ou igual a 01/01/1900 e menor ou igual à data atual.");
    }
} 
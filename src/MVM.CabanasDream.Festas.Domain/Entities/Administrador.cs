using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festas.Domain.Entities.Common;
using MVM.CabanasDream.Festas.Domain.Enum;

namespace MVM.CabanasDream.Festas.Domain.Entities;

public class Administrador : Pessoa
{
    private List<Festa> _festas = new();

    public Administrador(string nome, DateTime dataNascimento, string cpf, string rg, ENivelPermissao nivelPermissao)
        : base(nome, dataNascimento, cpf, rg)
    {
        NivelPermissao = nivelPermissao;
        
        Validar();
    }
    
    protected Administrador() {}
    
    public ENivelPermissao NivelPermissao { get; private set; }
    public IReadOnlyCollection<Festa> Festas => _festas;

    public void AlocarFesta(Festa festa)
    {
        if (festa == null)
            throw new DomainException("");
        
        _festas.Add(festa);
    }
    
    public override void Validar()
    {       
        // Nome
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome do cliente deve ser informado.");
        AssertionConcern.AssertArgumentLength(Nome, 0, 100, "O nome do cliente não deve conter mais que 100 caracteres.");

        // Cpf
        AssertionConcern.AssertArgumentNotEmpty(Cpf, "O CPF do cliente deve ser informado.");
        AssertionConcern.AssertArgumentLength(Cpf, 11, 11, "O CPF deve ter 11 dígitos.");
        AssertionConcern.AssertCpf(Cpf, "O CPF informado não é um CPF válido.");

        // Rg
        AssertionConcern.AssertArgumentNotEmpty(Rg, "O RG do administrador deve ser informado.");
        AssertionConcern.AssertArgumentLength(Rg, 9, 12, "O RG deve ter entre 9 e 12 dígitos.");

        // Data Nascimento
        AssertionConcern.AssertArgumentRange(DataNascimento, new DateTime(1900, 1, 1), DateTime.Now,
            "A data de nascimento deve ser maior ou igual a 01/01/1900 e menor ou igual à data atual.");
    }
} 
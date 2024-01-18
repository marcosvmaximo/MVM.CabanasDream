using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festas.Domain.Entities.Common;
using MVM.CabanasDream.Festas.Domain.ValueObjects;

namespace MVM.CabanasDream.Festas.Domain.Entities;

public class Cliente : Pessoa
{
    private List<Festa> _festas = new();

    public Cliente(string nome, DateTime dataNascimento, string cpf, string rg, Endereco endereco, Contato contato)
        : base(nome, dataNascimento, cpf, rg)
    {
        Endereco = endereco;
        Contato = contato;
        
        Validar();
    }
    
    protected Cliente(){}
    
    public Endereco Endereco { get; private set; }
    public Contato Contato { get; private set; }
    public IReadOnlyCollection<Festa> Festas => _festas;

    public void AlterarEndereco(Endereco endereco)
    {
        AssertionConcern.AssertArgumentNotNull(endereco, "O endereço deve informado é inválido.");

        Endereco = endereco;
    }

    public void AlterarContato(Contato contato)
    {
        AssertionConcern.AssertArgumentNotNull(contato, "O cotato informado é inválido.");

        Contato = contato;
    }

    public void AlocarFesta(Festa festa)
    {
        AssertionConcern.AssertArgumentNotNull(festa, "A festa informada é inválida.");

        _festas.Add(festa);
    }
    
    public sealed override void Validar()
    { 
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome do cliente deve ser informado.");
        AssertionConcern.AssertArgumentLength(Nome, 0, 100, "O nome do cliente não deve conter mais que 100 caracteres.");

        AssertionConcern.AssertArgumentRange(DataNascimento, new DateTime(1900, 1, 1), DateTime.Now, "A data de nascimento deve ser maior ou igual a 01/01/1900 e menor ou igual à data atual.");

        AssertionConcern.AssertArgumentNotEmpty(Cpf, "O CPF do cliente deve ser informado.");
        AssertionConcern.AssertArgumentLength(Cpf, 11, 11, "O CPF deve ter 11 dígitos.");
        AssertionConcern.AssertCpf(Cpf, "O CPF informado não é um CPF válido.");
        
        AssertionConcern.AssertArgumentNotEmpty(Rg, "O RG do administrador deve ser informado.");
        AssertionConcern.AssertArgumentLength(Rg, 9, 12, "O RG deve ter entre 9 e 12 dígitos.");
        
        AssertionConcern.AssertArgumentNotNull(Endereco, "O endereco deve estar associado a um cliente.");
        AssertionConcern.AssertArgumentNotNull(Contato, "O contato deve estar associado a um cliente.");
    }
}
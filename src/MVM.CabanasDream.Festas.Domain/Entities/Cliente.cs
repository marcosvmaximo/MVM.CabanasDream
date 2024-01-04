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
        AssertionConcern.AssertArgumentNotNull(endereco, "O endereco deve estar associado a um cliente.");

        Endereco = endereco;
    }

    public void AlterarContato(Contato contato)
    {
        AssertionConcern.AssertArgumentNotNull(contato, "O contato deve estar associado a um cliente.");

        Contato = contato;
    }
    
    public sealed override void Validar()
    { 
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome do cliente deve ser informado.");
        AssertionConcern.AssertArgumentLength(Nome, 0, 100, "O nome do cliente não deve conter mais que 100 caracteres.");
        
        AssertionConcern.AssertArgumentNotEmpty(Cpf, "O CPF do cliente deve ser informado.");
        AssertionConcern.AssertArgumentLength(Cpf, 11, 11, "O CPF deve ter 11 dígitos.");
        
        AssertionConcern.AssertArgumentRange(DataNascimento, new DateTime(1900, 1, 1), DateTime.Now, "A data de nascimento deve ser maior ou igual a 01/01/1900 e menor ou igual à data atual.");
        
        AssertionConcern.AssertArgumentNotNull(Endereco, "O endereco deve estar associado a um cliente.");
        AssertionConcern.AssertArgumentNotNull(Contato, "O contato deve estar associado a um cliente.");
    }
}
using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festa.Domain.ValueObjects;

namespace MVM.CabanasDream.Festa.Domain.Entities;

public class Cliente : Entity
{
    private List<Festa> _festas = new();

    public Cliente(string nome, string cpf, DateTime dataNascimento, Endereco endereco, Contato contato)
    {
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Endereco = endereco;
        Contato = contato;
        
        Validar();
    }

    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public Endereco Endereco { get; private set; }
    public Contato Contato { get; private set; }
    public IReadOnlyCollection<Festa> Festas => _festas;
    
    public sealed override void Validar()
    { 
        // Nome
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome do cliente deve ser informado.");
        AssertionConcern.AssertArgumentLength(Nome, 0, 100, "O nome do cliente não deve conter mais que 100 caracteres.");

        // Cpf
        AssertionConcern.AssertArgumentNotEmpty(Cpf, "O CPF do cliente deve ser informado.");
        AssertionConcern.AssertArgumentLength(Cpf, 11, 11, "O CPF deve ter 11 dígitos.");

        // Data Nascimento
        AssertionConcern.AssertArgumentRange(DataNascimento, new DateTime(1900, 1, 1), DateTime.Now, "A data de nascimento deve ser maior ou igual a 01/01/1900 e menor ou igual à data atual.");
        
        // Endereco
        AssertionConcern.AssertArgumentNotNull(Endereco, "O endereco deve estar associado a um cliente.");
        
        // Contato
        AssertionConcern.AssertArgumentNotNull(Contato, "O endereco deve estar associado a um cliente.");

    }
}
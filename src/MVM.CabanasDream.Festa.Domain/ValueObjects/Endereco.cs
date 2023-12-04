using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Validation;

namespace MVM.CabanasDream.Festa.Domain.ValueObjects;

public class Endereco : ValueObject
{
    public Endereco(string cep, string logradouro, string bairro, string cidade, string estado, string pais)
    {
        Cep = cep;
        Logradouro = logradouro;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Pais = pais;
        
        Validar();
    }

    public string Cep { get; private set; }
    public string Logradouro { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public string Pais { get; private set; }
    
    public sealed override void Validar()
    {
        AssertionConcern.AssertArgumentNotEmpty(Cep, "O CEP não pode ser vazio");
        AssertionConcern.AssertArgumentLength(Cep, 8, "O CEP deve conter 8 caracteres");

        AssertionConcern.AssertArgumentNotEmpty(Logradouro, "O logradouro não pode ser vazio");
        AssertionConcern.AssertArgumentLength(Logradouro, 1, 100, "O logradouro deve ter entre 1 e 100 caracteres");

        AssertionConcern.AssertArgumentNotEmpty(Bairro, "O bairro não pode ser vazio");
        AssertionConcern.AssertArgumentLength(Bairro, 1, 50, "O bairro deve ter entre 1 e 50 caracteres");

        AssertionConcern.AssertArgumentNotEmpty(Cidade, "A cidade não pode ser vazia");
        AssertionConcern.AssertArgumentLength(Cidade, 1, 50, "A cidade deve ter entre 1 e 50 caracteres");

        AssertionConcern.AssertArgumentNotEmpty(Estado, "O estado não pode ser vazio");
        AssertionConcern.AssertArgumentLength(Estado, 2, "O estado deve conter 2 caracteres");

        AssertionConcern.AssertArgumentNotEmpty(Pais, "O país não pode ser vazio");
        AssertionConcern.AssertArgumentLength(Pais, 1, 50, "O país deve ter entre 1 e 50 caracteres");
    }
}
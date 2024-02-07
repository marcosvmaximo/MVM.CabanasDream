using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Core.Validation;
using MVM.CabanasDream.Festas.Domain.TemaContext.Enum;
using MVM.CabanasDream.Festas.Domain.TemaContext.ValueObjects;

namespace MVM.CabanasDream.Festas.Domain.TemaContext.Entities;

public class Produto : Entity
{
    public Produto(string nome, string numeroSerie, ECategoriaProduto categoria, Valor valor)
    {
        Nome = nome;
        NumeroDeSerie = numeroSerie;
        Categoria = categoria;
        Valor = valor;
        
        Validar();
    }
    
    protected Produto(){}

    public string Nome { get; private set; }
    public ECategoriaProduto Categoria { get; private set; }
    public string NumeroDeSerie { get; private set; }
    public Valor Valor { get; private set; }
    public Tema? Tema { get; private set; }  
    public Guid? TemaId { get; private set; }
    public bool Alocado { get; private set; }

    public sealed override void Validar()
    {
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome do produto deve ser informado.");
        AssertionConcern.AssertArgumentLength(Nome, 0, 100, "O nome do produto não deve conter mais que 100 caracteres.");
        AssertionConcern.AssertArgumentNotEmpty(NumeroDeSerie, "O número de serie deve ser informado.");
        AssertionConcern.AssertArgumentLength(NumeroDeSerie, 5, 5, "O número de série deve ter exatamente 5 dígitos.");
        AssertionConcern.AssertArgumentNotNull(Valor, "O produto deve possuir valor válido");
    }

    public void SeAlocarAoTema(Tema tema)
    {
        if (tema == null)
            throw new DomainException("Erro ao tentar alocar um produto à um Tema. Tema inválido");

        Tema = tema;
        TemaId = tema.Id;

        Alocado = true;
    }
}
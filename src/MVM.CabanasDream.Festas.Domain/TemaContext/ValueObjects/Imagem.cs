using MVM.CabanasDream.Core.Domain;
using MVM.CabanasDream.Core.Validation;

namespace MVM.CabanasDream.Festas.Domain.TemaContext.ValueObjects;

public class Imagem : ValueObject
{
    public Imagem(string nome, string upload)
    {
        Nome = nome;
        Upload = upload;
        
        Validar();
    }
    
    public string Nome { get; private set; }
    public string Upload { get; private set; }
    
    public override void Validar()
    {
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome da Imagem precisa ser informado.");
        AssertionConcern.AssertArgumentLength(Nome, 1000, "O nome da Imagem não pode ser maior que 1000 caracteres");
        
        AssertionConcern.AssertArgumentNotEmpty(Upload, "A Imagem precisa ser informada.");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Nome;
        yield return Upload;
    }
}
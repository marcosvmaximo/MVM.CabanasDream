using MVM.CabanasDream.Festas.Domain.TemaContext.Enum;

namespace MVM.CabanasDream.Festas.Application.ViewModels.Temas;

public class ProdutoViewModel
{
    public string Nome { get; set; }
    public string NumeroDeSerie { get; set; }
    public int Categoria { get; set; }
    public decimal ValorCompra { get; set; }
    public decimal ValorLocacao { get; set; }
}
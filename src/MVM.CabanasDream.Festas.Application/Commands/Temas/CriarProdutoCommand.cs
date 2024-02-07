using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.Domain.TemaContext.Enum;

namespace MVM.CabanasDream.Festas.Application.Commands.Temas;

public class CriarProdutoCommand : Command
{
    public string Nome { get; set; }
    public ECategoriaProduto Categoria { get; set; }
    public decimal ValorCompra { get; set; }
    public decimal ValorLocacao { get; set; }
}
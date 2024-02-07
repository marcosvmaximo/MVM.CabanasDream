using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.Application.ViewModels.Temas;

namespace MVM.CabanasDream.Festas.Application.Commands.Temas;

public class CriarTemaCommand : Command
{
    public string Nome { get; set; }
    public decimal PrecoBase { get; set; }
    public string Imagem { get; set; }
    public string ImagemUpload { get; set; }
    public string? Descricao { get; set; }
    public IEnumerable<Guid> Produtos { get; set; }
}
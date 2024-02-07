namespace MVM.CabanasDream.Festas.Application.ViewModels.Temas;

public class TemaViewModel
{
    public string Nome { get; set; }
    public decimal PrecoBase { get; set; }
    public string Imagem { get; set; }
    public string ImagemUpload { get; set; }
    public string? Descricao { get; set; }
    public IEnumerable<ProdutoViewModel> Produtos { get; set; }
}
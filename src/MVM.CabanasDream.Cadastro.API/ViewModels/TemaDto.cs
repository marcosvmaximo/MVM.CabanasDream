namespace MVM.CabanasDream.Cadastro.API.ViewModels;

public record TemaDto(Guid Id, string Nome, string Descricao, decimal PrecoBase, string Imagem, string ImagemUpload, bool Disponibilidade, IEnumerable<ProdutoDto> produtos);
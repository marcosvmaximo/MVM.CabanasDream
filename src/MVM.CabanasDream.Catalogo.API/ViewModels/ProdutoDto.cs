using MVM.CabanasDream.Catalogo.API.Enum;

namespace MVM.CabanasDream.Catalogo.API.ViewModels;

public record ProdutoDto(Guid Id, string Nome, ETipoProduto Tipo);
using MVM.CabanasDream.Cadastro.API.Enum;

namespace MVM.CabanasDream.Cadastro.API.ViewModels;

public record ProdutoDto(Guid Id, string Nome, ETipoProduto Tipo);
using MVM.CabanasDream.Festas.Application.ViewModels.Temas;
using MVM.CabanasDream.Festas.Domain.Entities;

namespace MVM.CabanasDream.Festas.Application.ViewModels.Festas;

public record FestaViewModel(string Tema, string Cliente, int QuantidadeParticipantes,
    DateTime DataRealizacao, DateTime DataRetirada, DateTime DataDevolucao, decimal Valor, decimal Multa,
    IReadOnlyCollection<ProdutoViewModel> Produtos);

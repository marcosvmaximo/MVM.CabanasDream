using MVM.CabanasDream.Festas.Domain.Entities;

namespace MVM.CabanasDream.Festas.Application.ViewModels;

public record CriarFestaViewModel(string Tema, string Cliente, int QuantidadeParticipantes,
    DateTime DataRealizacao, DateTime DataRetirada, DateTime DataDevolucao, decimal Valor, decimal Multa,
    IReadOnlyCollection<Produto> Produtos);

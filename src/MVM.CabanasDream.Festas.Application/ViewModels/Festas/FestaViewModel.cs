namespace MVM.CabanasDream.Festas.Application.ViewModels.Festas;

public record FestaViewModel(string Tema, int QuantidadeParticipantes,
    DateTime DataRealizacao, DateTime DataRetirada, DateTime DataDevolucao);

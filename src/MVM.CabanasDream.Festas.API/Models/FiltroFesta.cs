using MVM.CabanasDream.Festas.Domain.Enum;

namespace MVM.CabanasDream.Festas.API.Models;

public class FiltroFesta
{
    public EStatusFesta Status { get; set; }
    public DateTime DataRealizacao { get; set; }
}
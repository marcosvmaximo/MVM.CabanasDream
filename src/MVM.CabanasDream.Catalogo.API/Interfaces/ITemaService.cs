using MVM.CabanasDream.Catalogo.API.ViewModels;

namespace MVM.CabanasDream.Catalogo.API.Interfaces;

public interface ITemaService
{
    Task<TemaDto> CadastrarTema(TemaViewModel model);
}
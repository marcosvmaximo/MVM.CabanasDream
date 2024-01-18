using MVM.CabanasDream.Cadastro.API.ViewModels;

namespace MVM.CabanasDream.Cadastro.API.Interfaces;

public interface ITemaService
{
    Task<TemaDto> CadastrarTema(TemaViewModel model);
}
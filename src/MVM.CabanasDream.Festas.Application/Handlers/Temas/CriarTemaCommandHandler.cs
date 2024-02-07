using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Festas.Application.Commands.Temas;
using MVM.CabanasDream.Festas.Application.FluentValidations.Temas;
using MVM.CabanasDream.Festas.Application.ViewModels.Temas;
using MVM.CabanasDream.Festas.Domain.Interfaces;
using MVM.CabanasDream.Festas.Domain.TemaContext;
using MVM.CabanasDream.Festas.Domain.TemaContext.ValueObjects;

namespace MVM.CabanasDream.Festas.Application.Handlers.Temas;

public class CriarTemaCommandHandler : Handler<CriarTemaCommand>
{
    private readonly ITemaRepository _repository;

    public CriarTemaCommandHandler(IMessageBus messager, ITemaRepository repository) : base(messager)
    {
        _repository = repository;
    }

    public override async Task<CommandResponse> Handle(CriarTemaCommand message, CancellationToken cancellationToken)
    {
        bool commandIsValid = ValidarComando<CriarTemaCommandValidator>(message);
        if (!commandIsValid) return ReturnResponse();
        
        var tema = MapTema(message);
        
        foreach (Guid idProduto in message.Produtos)
        {
            var produto = await _repository.ObterProdutoPorId(idProduto);
            
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (produto == null || produto.Alocado)
            {
                AddError(produto.Nome,$"O Produto {produto.Nome} já está alocado à festa {produto.Tema.Nome}.");
                return ReturnResponse();
            }
            
            tema.AdicionarProduto(produto);
        }
        
        await _repository.Adicionar(tema);
        await _repository.UnityOfWork.Commit();
        
        return ReturnResponse(MapViewModel(tema)); 
    }

    private Tema MapTema(CriarTemaCommand command)
    {
        Imagem imagem = new(command.Imagem, command.ImagemUpload);
        
        return new(command.Nome, command.PrecoBase, imagem, command.Descricao);
    }

    private TemaViewModel MapViewModel(Tema tema)
    {
        return new TemaViewModel();
    }
}
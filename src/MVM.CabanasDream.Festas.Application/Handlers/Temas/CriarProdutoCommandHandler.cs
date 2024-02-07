using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Festas.Application.Commands.Temas;
using MVM.CabanasDream.Festas.Application.FluentValidations.Temas;
using MVM.CabanasDream.Festas.Application.ViewModels.Temas;
using MVM.CabanasDream.Festas.Domain.Interfaces;
using MVM.CabanasDream.Festas.Domain.TemaContext.Entities;
using MVM.CabanasDream.Festas.Domain.TemaContext.ValueObjects;

namespace MVM.CabanasDream.Festas.Application.Handlers.Temas;

public class CriarProdutoCommandHandler : Handler<CriarProdutoCommand>
{
    private readonly ITemaRepository _repository;

    public CriarProdutoCommandHandler(ITemaRepository repository, IMessageBus messager) : base(messager)
    {
        _repository = repository;
    }

    public override async Task<CommandResponse> Handle(CriarProdutoCommand message, CancellationToken cancellationToken)
    {
        bool isValid = ValidarComando<CriarProdutoCommandValidator>(message);
        if (!isValid) return ReturnResponse();

        var produto = MapProduto(message);

        await _repository.AdicionarProduto(produto);
        await _repository.UnityOfWork.Commit();

        return ReturnResponse(MapViewModel(produto));
    }

    private ProdutoViewModel MapViewModel(Produto produto)
    {
        return new ProdutoViewModel()
        {
            Nome = produto.Nome,
            NumeroDeSerie = produto.NumeroDeSerie,
            Categoria = (int)produto.Categoria,
            ValorCompra = produto.Valor.ValorCompra,
            ValorLocacao = produto.Valor.ValorLocacao
        };
    }

    private Produto MapProduto(CriarProdutoCommand message)
    {
        var valores = new Valor(message.ValorCompra, message.ValorLocacao);
        
        return new Produto(message.Nome, GenerateSerialNumber(), message.Categoria, valores);
    }

    private string GenerateSerialNumber()
    {
        return new string(Guid.NewGuid().ToString().Where(char.IsLetterOrDigit).ToArray()).Substring(0, 5).ToUpper();
    }
}
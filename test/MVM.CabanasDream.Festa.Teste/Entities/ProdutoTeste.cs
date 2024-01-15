using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Festas.Domain.Entities;
using MVM.CabanasDream.Festas.Domain.Enum;

namespace MVM.CabanasDream.Festa.Teste.Entities;

public class ProdutoTeste
{
    private Tema _temaValido = new Tema("Barbie", 199m, "Tema incrível inspirado na boca e filmes da Barbie");
    
    [Fact(DisplayName = "Deve criar o Produto corretamente")]
    public void DevePassarAo_CriarProdutoValido()
    {
        // Arrange (Criar objetos necessários)
        var nome = "Almofada da Barbie";
        var tema = new Tema("Barbie", 199m, "Tema incrível inspirado na boca e filmes da Barbie");
        var numeroDeSerie = new Random().Next(10000, 99999).ToString();
        var valorCompra = 10.99m;
        var valorLocacao = 5.99m;
        
        // Act
        var exception = Record.Exception(() =>
        {
            var produto = new Produto(nome, ETipoProduto.Almofadas, numeroDeSerie, valorCompra, valorLocacao, tema);
        });

        // Assert
        Assert.Null(exception);
    }

    [Theory(DisplayName = "Deve falhar ao criar um Produto com nomes inválidos")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. ")]
    public void DeveFalharAo_CriarProduto_NomesInvalidos(string? nome)
    {
        // Arrange
        var numeroDeSerie = new Random().Next(10000, 99999).ToString();
        var valorCompra = 10.99m;
        var valorLocacao = 5.99m;
        
        // Act
        var act = () =>
        {
            var produto = new Produto(nome, ETipoProduto.Almofadas, numeroDeSerie, valorCompra, valorLocacao,
                _temaValido);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }
    
    [Theory(DisplayName = "Deve criar o Produto corretamente com numero de serie vázios ou nulos")]
    [InlineData("")]
    [InlineData(null)]
    public void DevePassarAo_CriarProdutoCom_NumeroDeSerieVazioOuNulo(string? numeroDeSerie)
    {
        // Arrange
        var nome = "Almofada da Barbie";
        var valorCompra = 10.99m;
        var valorLocacao = 5.99m;
        
        // Act
        var exception = Record.Exception(() =>
        {
            var produto = new Produto(nome, ETipoProduto.Almofadas, numeroDeSerie, valorCompra, valorLocacao,
                _temaValido);
        });
        
        // Assert
        Assert.Null(exception);
    }
    
    [Theory(DisplayName = "Deve falhar ao criar o Produto com valor de compra inválido")]
    [InlineData(-10)]
    [InlineData(9999999)]
    public void DeveFalharAo_CriarProdutoCom_ValorCompraInvalido(decimal valorCompra)
    {
        // Arrange
        var nome = "Almofada da Barbie";
        var numeroDeSerie = new Random().Next(10000, 99999).ToString();
        var valorLocacao = 5.99m;
        
        // Act
        var act = () =>
        {
            var produto = new Produto(nome, ETipoProduto.Almofadas, numeroDeSerie, valorCompra, valorLocacao,
                _temaValido);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }
    
    [Theory(DisplayName = "Deve falhar ao criar o Produto com valor de compra inválido")]
    [InlineData(-10)]
    [InlineData(9999999)]
    public void DevFalharAo_CriarProdutoCom_ValorLocacaoInvalido(decimal valorLocacao)
    {
        // Arrange
        var nome = "Almofada da Barbie";
        var numeroDeSerie = new Random().Next(10000, 99999).ToString();
        var valorCompra = 10.99m;
        
        // Act
        var act = () =>
        {
            var produto = new Produto(nome, ETipoProduto.Almofadas, numeroDeSerie, valorCompra, valorLocacao,
                _temaValido);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }

    [Fact(DisplayName = "Deve falhar ao criar Produto com Tema inválido")]
    public void DeveFalharAo_CriarProdutoCom_TemaInvalido()
    {
        // Arrange
        var nome = "Almofada da Barbie";
        var numeroDeSerie = new Random().Next(10000, 99999).ToString();
        var valorCompra = 10.99m;
        var valorLocacao = 5.99m;
        
        // Act
        var act = () =>
        {
            var produto = new Produto(nome, ETipoProduto.Almofadas, numeroDeSerie, valorCompra, valorLocacao,
                null);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }
}   

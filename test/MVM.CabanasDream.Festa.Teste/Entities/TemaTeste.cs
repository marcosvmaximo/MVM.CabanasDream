using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Festas.Domain.Entities;
using MVM.CabanasDream.Festas.Domain.Enum;
using Xunit.Sdk;

namespace MVM.CabanasDream.Festa.Teste.Entities;

public class TemaTeste
{
    [Fact(DisplayName = "Deve criar um Tema válido corretamente")]
    public void DevePassarAo_CriarTemaValido()
    {
        // Arrange
        var nome = "Barbie";
        var precoBase = 109m;
        var descricao = "";
        
        // Act
        var exception = Record.Exception(() =>
        {
            var tema = new Tema(nome, precoBase, descricao);
        });
        
        // Assert
        Assert.Null(exception);
    }

    [Theory(DisplayName = "Deve falhar ao tentar criar um Tema, que possua nomes inválidos")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. ")]
    public void DeveFalharAo_CriarTemaCom_NomeInvalido(string nome)
    {
        // Arrange
        var precoBase = 109m;
        var descricao = "";
        
        // Act
        var act = () =>
        {
            var tema = new Tema(nome, precoBase, descricao);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);  
    }

    [Theory(DisplayName = "Deve falhar ao tentar criar um Tema, que possua preço base inválido")]
    [InlineData(-10)]
    [InlineData(99999)]
    [InlineData(10002)]
    public void DeveFalharAoCriarTemaCom_PrecoBaseInvalido(decimal precoBase)
    {
        // Arrange
        var nome = "Barbie";
        var descricao = "";
        
        // Act
        var act = () =>
        {
            var tema = new Tema(nome, precoBase, descricao);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);  
    }

    [Fact(DisplayName = "Deve possuir Disponibilidade falsa ao Indisponibilizar o Tema")]
    public void DevePassarAo_IndisponibilizarTema_DisponibilidadeDeveSerFalsa()
    {
        // Arrange
        var nome = "Barbie";
        var precoBase = 109m;
        var descricao = "";
        
        // Act
        var tema = new Tema(nome, precoBase, descricao);
        tema.Indisponibilizar();
        
        // Assert
        Assert.False(tema.Disponibilidade);  
    }
    
    [Fact(DisplayName = "Deve possuir Disponibilidade verdadeira ao Disponibilizar o Tema")]
    public void DevePassarAo_DsponibilizarTema_DisponibilidadeDeveSerVerdadeira()
    {
        // Arrange
        var nome = "Barbie";
        var precoBase = 109m;
        var descricao = "";
        
        // Act
        var tema = new Tema(nome, precoBase, descricao);
        tema.Indisponibilizar();
        tema.Disponibilizar();
        
        // Assert
        Assert.True(tema.Disponibilidade);  
    }
    
    [Theory(DisplayName = "Deve criar um Tema valido com descrição nula ou vázia")]
    [InlineData(null)]
    [InlineData("")]
    public void DevePassarAo_CriarTemaCom_DescricaoNulaOuVazia(string descricao)
    {
        // Arrange
        var nome = "Barbie";
        var precoBase = 109m;
        
        // Act
        var exception = Record.Exception(() =>
        {
            var tema = new Tema(nome, precoBase, descricao);
        });
        
        // Assert
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Deve falhar ao criar um Tema, que possua uma Descrição maior que 500 caracteres")]
    public void DeveFalharAo_CriarTemaCom_DescricaoMaiorQue500()
    {
        // Arrange
        var nome = "Barbie";
        var precoBase = 109m;
        var descricao = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        
        // Act
        var act = () =>
        {
            var tema = new Tema(nome, precoBase, descricao);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);  
    }
    
    [Fact(DisplayName = "Deve falhar ao criar um Tema que possua disponibilidade falsa")]
    public void DeveFalharAo_CriarTemaCom_DisponibilidadeFalsa()
    {
        // Arrange
        var nome = "Barbie";
        var precoBase = 109m;
        var descricao = "";
        
        // Act
        var tema = new Tema(nome, precoBase, descricao);
        
        // Assert
        Assert.True(tema.Disponibilidade); 
    }

    [Fact(DisplayName = "Deve falhar ao tentar adicionar um produto extra inválido")]
    public void DeveFalharAo_TentarAdicionarProdutoExtraInvalido()
    {
        // Arrange
        var nome = "Barbie";
        var precoBase = 109m;
        var descricao = "";

        var tema = new Tema(nome, precoBase, descricao);
        Produto produtoExtra = null;
        
        // Act
        var act = () =>
        {
            tema.AdicionarProdutoExtra(produtoExtra);
        };
        
        // Assert
        Assert.Throws<DomainException>(act); 
    }
    
    [Fact(DisplayName = "Deve falhar ao criar um Tema que possua disponibilidade falsa")]
    public void DeveFalharAo_TentarAdicionarProdutoExtra_ComTemaIndisponivel()
    {
        // Arrange
        var nome = "Barbie";
        var precoBase = 109m;
        var descricao = "";

        var tema = new Tema(nome, precoBase, descricao);
        tema.Indisponibilizar();

        Produto produtoExtra = new Produto("Almofada da barbie", ETipoProduto.Almofadas, "10239", 29.99m, 20m, tema);
        
        // Act
        var act = () =>
        {
            tema.AdicionarProdutoExtra(produtoExtra);
        };
        
        // Assert
        Assert.Throws<DomainException>(act); 
    }

    public void DevePassarAo_AdicionarProdutosDeValor20_QueAumentePrecoBaseEm20()
    {
        // Arrange
        var nome = "Barbie";
        var precoBase = 109m;
        var descricao = "";

        var tema = new Tema(nome, precoBase, descricao);
        var produtoExtra = new Produto("Almofada da barbie", ETipoProduto.Almofadas, "10239", 29.99m, 20m, tema);
        
        // Act
        var act = () =>
        {
            tema.AdicionarProdutoExtra(produtoExtra);
        };
        
        // Assert
        Assert.Equal(precoBase + produtoExtra.ValorLocacao, tema.PrecoBase); 
    }

    [Theory(DisplayName = "Deve alterar corretamente o Preço Base para o novo preço base")]
    [InlineData(10)]
    [InlineData(100)]
    public void DevePassarAo_AlterarPrecoBase_ParaNovosPrecos(decimal novoPreco)
    {
        // Arrange
        var nome = "Barbie";
        var precoBase = 109m;
        var descricao = "";
        
        // Act
        var tema = new Tema(nome, precoBase, descricao);
        tema.AlterarPreco(novoPreco);
        
        // Assert
        Assert.Equal( novoPreco, tema.PrecoBase); 
    }
}
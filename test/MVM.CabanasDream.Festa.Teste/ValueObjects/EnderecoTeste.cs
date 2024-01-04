using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Festas.Domain.ValueObjects;

namespace MVM.CabanasDream.Festa.Teste.ValueObjects;

public class EnderecoTeste
{
    [Fact]
    public void TestarCriacaoEndereco_ComValoresValidos_DevePassar()
    {
        // Arrange
        string cep = "12345678";
        string logradouro = "Rua Exemplo";
        string bairro = "Bairro Exemplo";
        string cidade = "Cidade Exemplo";
        string estado = "ES";
        string pais = "Brasil";

        // Act
        var endereco = new Endereco(cep, logradouro, bairro, cidade, estado, pais);

        // Assert
        Assert.NotNull(endereco);
    }

    [Fact]
    public void TestarValidacaoCepInvalido_ComCepVazio_DeveLancarExcecao()
    {
        // Arrange
        string cep = "";

        // Act & Assert
        Assert.Throws<DomainException>(() => new Endereco(cep, "Rua Exemplo", "Bairro", "Cidade", "ES", "Brasil"));
    }

    [Fact]
    public void TestarValidacaoLogradouroInvalido_ComLogradouroMaiorQue100Caracteres_DeveLancarExcecao()
    {
        // Arrange
        string logradouro = new string('X', 101); // Logradouro maior que 100 caracteres

        // Act & Assert
        Assert.Throws<DomainException>(() => new Endereco("12345678", logradouro, "Bairro", "Cidade", "ES", "Brasil"));
    }

    [Fact]
    public void TestarValidacaoBairroInvalido_ComBairroMaiorQue50Caracteres_DeveLancarExcecao()
    {
        // Arrange
        string bairro = new string('X', 51); // Bairro maior que 50 caracteres

        // Act & Assert
        Assert.Throws<DomainException>(() => new Endereco("12345678", "Rua Exemplo", bairro, "Cidade", "ES", "Brasil"));
    }

    [Fact]
    public void TestarValidacaoCidadeInvalida_ComCidadeMaiorQue50Caracteres_DeveLancarExcecao()
    {
        // Arrange
        string cidade = new string('X', 51); // Cidade maior que 50 caracteres

        // Act & Assert
        Assert.Throws<DomainException>(() => new Endereco("12345678", "Rua Exemplo", "Bairro", cidade, "ES", "Brasil"));
    }

    [Fact]
    public void TestarValidacaoEstadoInvalido_ComEstadoVazio_DeveLancarExcecao()
    {
        // Arrange
        string estado = ""; // Estado vazio

        // Act & Assert
        Assert.Throws<DomainException>(() => new Endereco("12345678", "Rua Exemplo", "Bairro", "Cidade", estado, "Brasil"));
    }

    [Fact]
    public void TestarValidacaoPaisInvalido_ComPaisMaiorQue50Caracteres_DeveLancarExcecao()
    {
        // Arrange
        string pais = new string('X', 51); // País maior que 50 caracteres

        // Act & Assert
        Assert.Throws<DomainException>(() => new Endereco("12345678", "Rua Exemplo", "Bairro", "Cidade", "ES", pais));
    }

    [Fact]
    public void TestarIgualdadeEntreDoisEnderecoIguais_DeveSerIgual()
    {
        // Arrange
        var endereco1 = new Endereco("12345678", "Rua Exemplo", "Bairro", "Cidade", "ES", "Brasil");
        var endereco2 = new Endereco("12345678", "Rua Exemplo", "Bairro", "Cidade", "ES", "Brasil");

        // Act & Assert
        Assert.Equal(endereco1, endereco2);
    }

    [Fact]
    public void TestarDesigualdadeEntreDoisEnderecoDiferentes_DeveSerDiferente()
    {
        // Arrange
        var endereco1 = new Endereco("12345678", "Rua Exemplo", "Bairro", "Cidade", "ES", "Brasil");
        var endereco2 = new Endereco("87654321", "Rua Exemplo", "Bairro", "Cidade", "ES", "Brasil");

        // Act & Assert
        Assert.NotEqual(endereco1, endereco2);
    }
    
    //
    // [Fact]
    // public void TestarImutabilidadeDoValueObject_AposValidacao_DeveSerImutavel()
    // {
    //     // Arrange
    //     var endereco = new Endereco("12345678", "Rua Exemplo", "Bairro", "Cidade", "ES", "Brasil");
    //
    //     // Act
    //     // Tente modificar os valores após a validação
    //     Assert.Throws<InvalidOperationException>(() => { endereco.Cep = "87654321"; });
    //     Assert.Throws<InvalidOperationException>(() => { endereco.Logradouro = "Nova Rua"; });
    //
    //     // Assert
    //     // Verifique se os valores originais não foram alterados
    //     Assert.Equal("12345678", endereco.Cep);
    //     Assert.Equal("Rua Exemplo", endereco.Logradouro);
    // }
}
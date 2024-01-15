using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Festas.Domain.ValueObjects;

namespace MVM.CabanasDream.Festa.Teste.ValueObjects;

public class ContatoTeste
{
    [Fact]
    public void TestarCriacaoDoContatoComValoresValidos_DevePassar()
    {
        // Arrange
        var ddd = "11";
        var telefone = "987654321";
        var email = "teste@email.com";

        // Act
        var contato = new Contato(ddd, telefone, email);

        // Assert
        Assert.Equal(ddd, contato.Ddd);
        Assert.Equal(telefone, contato.Telefone);
        Assert.Equal(email, contato.Email);
    }
        [Fact]
        public void TestarCriacaoDoValueObject_ComValoresValidos_DevePassar()
        {
            // Arrange
            var contato1 = new Contato("11", "987654321", "teste@email.com");
            var contato2 = new Contato("11", "987654321", "teste@email.com");

            // Act
            // Assert
            Assert.Equal(contato1, contato2);
            Assert.Equal(contato1.GetHashCode(), contato2.GetHashCode());
        }

        [Fact]
        public void TestarValidacaoDeDDDInvalido_DeveLancarExcecao()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<DomainException>(() => new Contato("1", "987654321", "teste@email.com"));
        }

        [Fact]
        public void TestarValidacaoDeTelefoneInvalido_DeveLancarExcecao()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<DomainException>(() => new Contato("11", "123", "teste@email.com"));
        }

        [Fact]
        public void TestarValidacaoDeEmailInvalido_DeveLancarExcecao()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<DomainException>(() => new Contato("11", "987654321", "invalido"));
        }

        [Fact]
        public void TestarIgualdadeEntreDoisContatosIguais_DeveRetornarTrue()
        {
            // Arrange
            var contato1 = new Contato("11", "987654321", "teste@email.com");
            var contato2 = new Contato("11", "987654321", "teste@email.com");

            // Act
            // Assert
            Assert.True(contato1.Equals(contato2));
            Assert.False(contato1 == contato2);
            Assert.True(contato1 != contato2);
        }

        [Fact]
        public void TestarIgualdadeEntreDoisContatosDiferentes_DeveRetornarFalse()
        {
            // Arrange
            var contato1 = new Contato("11", "987654321", "teste@email.com");
            var contato2 = new Contato("21", "987654321", "outro@email.com");

            // Act
            // Assert
            Assert.False(contato1.Equals(contato2));
            Assert.False(contato1 == contato2);
            Assert.True(contato1 != contato2);
        }
}
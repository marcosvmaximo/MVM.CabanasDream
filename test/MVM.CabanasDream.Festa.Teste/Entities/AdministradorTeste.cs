using System.Runtime.InteropServices;
using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Festas.Domain.Entities;
using MVM.CabanasDream.Festas.Domain.Enum;
using MVM.CabanasDream.Festas.Domain.ValueObjects;

namespace MVM.CabanasDream.Festa.Teste.Entities;

public class AdministradorTeste
{
    private string _cpfValido = "95749005097";
    private string _rgValido = "347657394";
    
    [Fact(DisplayName = "Deve criar um Administrador válido corretamente")]
    public void DevePassarAo_CriarAdministradorValido()
    {
        // Arrange
        var nome = "Maria";
        var dataNascimento = new DateTime(1990, 10, 09);

        // Act
        var exception = Record.Exception(() =>
        {
            var administrador = new Administrador(nome, dataNascimento, _cpfValido, _rgValido, ENivelPermissao.Gerente);
        });
        
        // Assert
        Assert.Null(exception);
    }

    [Theory(DisplayName = "Deve falhar ao tentar criar um Administrador, que possua nome inválido")]
    [InlineData("")]
    [InlineData(null)]    
    [InlineData("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. ")]
    public void DeveFalharAo_CriarAdministradorCom_NomeInvalido(string nome)
    {
        // Arrange
        var dataNascimento = new DateTime(1990, 10, 09);
        
        // Act
        var act = () =>
        {
            var adm = new Administrador(nome, dataNascimento, _cpfValido, _rgValido, ENivelPermissao.Gerente);
        };

        // Assert
        Assert.Throws<DomainException>(act);
    }
    
    [Theory(DisplayName = "Deve falhar ao tentar criar um Administrador, que possua data de nascimento inválida")]
    [InlineData("1899-12-01")]
    [InlineData("1000-01-01")]
    [InlineData("2040-01-01")]
    public void DeveFalharAo_CriarAdministradorCom_DataNascimentoInvalida(string dataNascimento)
    {
        // Arrange
        var nome = "Maria";
        var dta = DateTime.ParseExact(dataNascimento, "yyyy-MM-dd", null);
        
        // Act
        var act = () =>
        {
            var adm = new Administrador(nome, dta, _cpfValido, _rgValido, ENivelPermissao.Gerente);
        };

        // Assert
        Assert.Throws<DomainException>(act);
    }
    
    [Theory(DisplayName = "Deve falhar ao tentar criar um Administrador, que possua CPF inválido")]
    [InlineData("")]
    [InlineData("100")]
    [InlineData("10023443243421")]
    [InlineData("12312312398")]
    [InlineData(null)]
    public void DeveFalharAo_CriarAdministradorCom_CpfInvalido(string cpf)
    {
        // Arrange
        var nome = "Maria";
        var dataNascimento = new DateTime(1990, 10, 09);

        // Act
        var act = () =>
        {
            var administrador = new Administrador(nome, dataNascimento, cpf, _rgValido, ENivelPermissao.Gerente);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }

    [Theory(DisplayName = "Deve falhar ao tentar criar um Administrador, que possua RG inválido")]
    [InlineData("")]
    [InlineData("100")]
    [InlineData("10023443243421")]
    [InlineData(null)]
    public void DeveFalharAo_CriarAdministrador_RgInvalido(string rg)
    {
        // Arrange
        var nome = "Maria";
        var dataNascimento = new DateTime(1990, 10, 09);

        // Act
        var act = () =>
        {
            var administrador = new Administrador(nome, dataNascimento, _cpfValido, rg, ENivelPermissao.Gerente);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }
    
    [Fact(DisplayName = "Deve falhar ao tentar alocar uma Festa inválida ao Administrador")]
    public void DeveFalharAo_AlocarUmaFestaInvalida()
    {
        // Arrange
        var nome = "Maria";
        var dataNascimento = new DateTime(1990, 10, 09);
        
        var administrador = new Administrador(nome, dataNascimento, _cpfValido, _rgValido, ENivelPermissao.Gerente);
        
        // Act
        var act = () =>
        {
            administrador.AlocarFesta(null);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }
    
    [Fact(DisplayName = "Deve alocar corretamente uma Festa válida ao Administrador")]
    public void DevePassarAo_AlocarUmaFestaValida()
    {
        
        // Arrange
        var nome = "Maria";
        var dataNascimento = new DateTime(1990, 10, 09);
        
        var administrador = new Administrador(nome, dataNascimento, _cpfValido, _rgValido, ENivelPermissao.Gerente);

        var festa = new Festas.Domain.Festa(
            10,
            DateTime.Now.AddDays(4),
            DateTime.Now.AddDays(1), 
            DateTime.Now.AddDays(5),
            CriarTemaValido(),
            CriarClienteValido(),
            administrador);
        
        // Act
        var exception = Record.Exception(() =>
        {
            administrador.AlocarFesta(festa);
        });
        
        // Assert
        Assert.Null(exception);
    }

    private Tema CriarTemaValido()
    {
        var nome = "Barbie";
        var precoBase = 109m;
        var descricao = "";

        return new Tema(nome, precoBase, descricao);
    }

    private Cliente CriarClienteValido()
    {
        var nome = "Cliente 1";
        var dataNascimento = new DateTime(1990, 01, 01);

        var contato = new Contato("41", "988501020", "teste@mail.com");
        var endereco = new Endereco("81800500", "Rua Oal", "Bairro", "Curitiba", "PR", "Brasil");
        
        return new Cliente(nome, dataNascimento, _cpfValido, _rgValido, endereco, contato);
    }
}
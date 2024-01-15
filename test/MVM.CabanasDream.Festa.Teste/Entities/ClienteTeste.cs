using MVM.CabanasDream.Core.Exceptions;
using MVM.CabanasDream.Festas.Domain.Entities;
using MVM.CabanasDream.Festas.Domain.Enum;
using MVM.CabanasDream.Festas.Domain.ValueObjects;

namespace MVM.CabanasDream.Festa.Teste.Entities;

public class ClienteTeste
{
    private string _cpfValido = "95749005097";
    private string _rgValido = "347657394";
    private Endereco _enderecoValido = new Endereco("81850500", "Rua Logradouro", "Bairro bairro", "Curitiba", "PR", "Brasil");
    private Contato _contatoValido = new Contato("41", "988501010", "teste@mail.com");
    
    [Fact(DisplayName = "Deve criar um Cliente válido corretamente")]
    public void DevePassarAo_CriarClienteValido()
    {
        // Arrange
        var nome = "Maria do Carmo";
        var dataNascimento = new DateTime(2000, 10, 09);

        // Act
        var exception = Record.Exception(() =>
        {
            var cliente = new Cliente(nome, dataNascimento, _cpfValido, _rgValido, _enderecoValido, _contatoValido);
        });
        
        // Assert
        Assert.Null(exception);
    }

    [Theory(DisplayName = "Deve falhar ao tentar criar um Cliente, que possua nome inválido")]
    [InlineData("")]
    [InlineData(null)]    
    [InlineData("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. ")]
    public void DeveFalharAo_CriarClienteCom_NomeInvalido(string nome)
    {
        // Arrange
        var dataNascimento = new DateTime(2000, 10, 09);
        
        // Act
        var act = () =>
        {
            var cliente = new Cliente(nome, dataNascimento, _cpfValido, _rgValido, _enderecoValido, _contatoValido);
        };

        // Assert
        Assert.Throws<DomainException>(act);
    }
    
    [Theory(DisplayName = "Deve falhar ao tentar criar um Cliente, que possua data de nascimento inválida")]
    [InlineData("1899-12-01")]
    [InlineData("1000-01-01")]
    [InlineData("2040-01-01")]
    public void DeveFalharAo_CriarClienteCom_DataNascimentoInvalida(string dataNascimento)
    {
        // Arrange
        var nome = "Maria do Carmo";
        var dta = DateTime.ParseExact(dataNascimento, "yyyy-MM-dd", null);
        
        // Act
        var act = () =>
        {
            var cliente = new Cliente(nome, dta, _cpfValido, _rgValido, _enderecoValido, _contatoValido);
        };

        // Assert
        Assert.Throws<DomainException>(act);
    }
    
    [Theory(DisplayName = "Deve falhar ao tentar criar um Cliente, que possua CPF inválido")]
    [InlineData("")]
    [InlineData("100")]
    [InlineData("10023443243421")]
    [InlineData("12312312398")]
    [InlineData(null)]
    public void DeveFalharAo_CriarClienteCom_CpfInvalido(string cpf)
    {
        // Arrange
        var nome = "Maria do Carmo";
        var dataNascimento = new DateTime(2000, 10, 09);

        // Act
        var act = () =>
        {
            var cliente = new Cliente(nome, dataNascimento, cpf, _rgValido, _enderecoValido, _contatoValido);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }

    [Theory(DisplayName = "Deve falhar ao tentar criar um Cliente, que possua RG inválido")]
    [InlineData("")]
    [InlineData("100")]
    [InlineData("10023443243421")]
    [InlineData(null)]
    public void DeveFalharAo_CriarCliente_RgInvalido(string rg)
    {
        // Arrange
        var nome = "Maria do Carmo";
        var dataNascimento = new DateTime(2000, 10, 09);

        // Act
        var act = () =>
        {
            var cliente = new Cliente(nome, dataNascimento, _cpfValido, rg, _enderecoValido, _contatoValido);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }
    
    [Fact(DisplayName = "Deve falhar ao tentar criar um Cliente, que possua um Endreco inválido")]
    public void DeveFalharAo_CriarClienteCom_EnderecoInvalido()
    {
        
        // Arrange
        var nome = "Maria do Carmo";
        var dataNascimento = new DateTime(2000, 10, 09);

        // Act
        var act = () =>
        {
            var cliente = new Cliente(nome, dataNascimento, _cpfValido, _rgValido, null, _contatoValido);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }

    [Fact(DisplayName = "Deve falhar ao tentar criar um Cliente, que possua um Contato inválido")]
    public void DeveFalharAo_CriarClienteCom_ContatoInvalido()
    {
        // Arrange
        var nome = "Maria do Carmo";
        var dataNascimento = new DateTime(2000, 10, 09);

        // Act
        var act = () =>
        {
            var cliente = new Cliente(nome, dataNascimento, _cpfValido, _rgValido, _enderecoValido, null);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }

    [Fact(DisplayName = "Deve falhar ao tentar alterar o endereço para um endereço inválido")]
    public void DeveFalharAo_TentarAlterarEndereco_ComEnderecoInvalido()
    {
        // Arrange
        var nome = "Maria do Carmo";
        var dataNascimento = new DateTime(2000, 10, 09);
        var cliente = new Cliente(nome, dataNascimento, _cpfValido, _rgValido, _enderecoValido, _contatoValido);

        // Act
        var act = () =>
        {
            cliente.AlterarEndereco(null);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }
    
    [Fact(DisplayName = "Deve falhar ao tentar alterar o contato para um contato inválido")]
    public void DeveFalharAo_TentarAlterarContato_ComContatoInvalido()
    {
        // Arrange
        var nome = "Maria do Carmo";
        var dataNascimento = new DateTime(2000, 10, 09);
        var cliente = new Cliente(nome, dataNascimento, _cpfValido, _rgValido, _enderecoValido, _contatoValido);

        // Act
        var act = () =>
        {
            cliente.AlterarContato(null);
        };
        
        // Assert
        Assert.Throws<DomainException>(act); 
    }

    [Fact(DisplayName = "Deve alterar corretamente o endereço para um novo")]
    public void DevePassarAo_AlterarEndereco_ParaEnderecoNovo()
    {
        // Arrange
        var nome = "Maria do Carmo";
        var dataNascimento = new DateTime(2000, 10, 09);
        var cliente = new Cliente(nome, dataNascimento, _cpfValido, _rgValido, _enderecoValido, _contatoValido);

        var novoEndereco = new Endereco("10500900", "Rua Nova", "Bairro Novo", "São Paulo", "Sp", "Brasil");
        
        // Act
        cliente.AlterarEndereco(novoEndereco);
        
        // Assert
        Assert.Equal(novoEndereco, cliente.Endereco); 
    }
    
    [Fact(DisplayName = "Deve alterar corretamente o contato para um novo")]
    public void DevePassarAoAlterarEndereco_ParaEnderecoNovo()
    {
        // Arrange
        var nome = "Maria do Carmo";
        var dataNascimento = new DateTime(2000, 10, 09);
        var cliente = new Cliente(nome, dataNascimento, _cpfValido, _rgValido, _enderecoValido, _contatoValido);

        var novoContato = new Contato("11", "9890807060", "new@mail.com");
        
        // Act
        cliente.AlterarContato(novoContato);
        
        // Assert
        Assert.Equal(novoContato, cliente.Contato); 
    }
    
    [Fact(DisplayName = "Deve falhar ao tentar alocar uma Festa inválida ao Cliente")]
    public void DeveFalharAo_AlocarUmaFestaInvalida()
    {
        // Arrange
        var nome = "Maria do Carmo";
        var dataNascimento = new DateTime(2000, 10, 09);
        
        var cliente = new Cliente(nome, dataNascimento, _cpfValido, _rgValido, _enderecoValido, _contatoValido);
        
        // Act
        var act = () =>
        {
            cliente.AlocarFesta(null);
        };
        
        // Assert
        Assert.Throws<DomainException>(act);
    }
    
    [Fact(DisplayName = "Deve alocar corretamente uma Festa válida ao Cliente")]
    public void DevePassarAo_AlocarUmaFestaValida()
    {
        // Arrange
        var nome = "Maria do Carmo";
        var dataNascimento = new DateTime(200, 10, 09);
        
        var cliente = new Cliente(nome, dataNascimento, _cpfValido, _rgValido, _enderecoValido, _contatoValido);

        var festa = new Festas.Domain.Festa(
            10,
            DateTime.Now.AddDays(4),
            DateTime.Now.AddDays(1), 
            DateTime.Now.AddDays(5),
            CriarTemaValido(),
            CriarClienteValido(),
            CriarAdministradorValido());
        
        // Act
        var exception = Record.Exception(() =>
        {
            cliente.AlocarFesta(festa);
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

    private Administrador CriarAdministradorValido()
    {
        var nome = "Maria";
        var dataNascimento = new DateTime(1990, 10, 09);
        
        return new Administrador(nome, dataNascimento, _cpfValido, _rgValido, ENivelPermissao.Gerente);
    }
}
using System.ComponentModel.DataAnnotations;

namespace MVM.CabanasDream.Cadastro.API.ViewModels;

public class TemaViewModel
{
    [Key]    
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(500, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Range(0, 10000, ErrorMessage = "O preço deve estar entre {0} e {1}")]
    public decimal PrecoBase { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(500, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Imagem { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(int.MaxValue, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string ImagemUpload { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public bool Disponibilidade { get; set; }
    
    public IEnumerable<ProdutoViewModel> Produtos { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using MVM.CabanasDream.Cadastro.API.Enum;
using MVM.CabanasDream.Cadastro.API.Models;

namespace MVM.CabanasDream.Cadastro.API.ViewModels;

public record ProdutoViewModel
{
    [Key]
    public Guid Id { get; set; }
};
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using MVM.CabanasDream.Catalogo.API.Enum;
using MVM.CabanasDream.Catalogo.API.Models;

namespace MVM.CabanasDream.Catalogo.API.ViewModels;

public record ProdutoViewModel
{
    [Key]
    public Guid Id { get; set; }
};
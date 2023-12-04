using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVM.CabanasDream.Cadastro.API.Data;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> opt) : base(opt)
    {
        
    }
}
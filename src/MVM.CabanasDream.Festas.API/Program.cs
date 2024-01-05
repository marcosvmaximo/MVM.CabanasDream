using Microsoft.EntityFrameworkCore;
using MVM.CabanasDream.Festas.API.Configurations;
using MVM.CabanasDream.Festas.API.Configurations.Services;
using MVM.CabanasDream.Festas.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddServicesExtensions();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(opt => opt
    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



var app = builder.Build();

// SwaggerConfig
app.UseSwaggerConfig(app.Environment);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVM.CabanasDream.Cadastro.API;
using MVM.CabanasDream.Cadastro.API.Data;
using MVM.CabanasDream.Cadastro.API.Interfaces;
using MVM.CabanasDream.Cadastro.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddServicesExtensions(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

// IOC
builder.Services.AddScoped<DataContext>();
builder.Services.AddTransient<ITemaRepository, TemaRepository>();
builder.Services.AddTransient<ITemaService, TemaService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


var app = builder.Build();

app.UseExceptionHandler("/error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Version 1.0");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "API Version 2.0");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
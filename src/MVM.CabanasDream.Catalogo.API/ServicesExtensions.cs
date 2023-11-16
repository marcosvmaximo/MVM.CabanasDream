
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVM.CabanasDream.Catalogo.API;

public static class ServicesExtensions
{
    public static IServiceCollection AddServicesExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        // Logging
        services.AddLogging();
        
        // Versioning
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(2, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV";
            opt.SubstituteApiVersionInUrl = true;
        });
        
        // Swagger
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Cabana's Dream Catalogo v1.0",
                Description = "Catalogo de exibição dos Temas",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Marcos Maximo",
                    Email = "marcosvinmaximo@gmail.com",
                    Url = new Uri("https://github.com/marcosvmaximo")
                },
                License = new OpenApiLicense
                {
                    Name = "Marcos Maximo",
                    Url = new Uri("https://github.com/marcosvmaximo")
                }
            });
            
            opt.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "Cabana's Dream Catalogo",
                Description = "Catalogo de exibição dos Temas e inserção",
                Version = "v2",
                Contact = new OpenApiContact
                {
                    Name = "Marcos Maximo",
                    Email = "marcosvinmaximo@gmail.com",
                    Url = new Uri("https://github.com/marcosvmaximo")
                },
                License = new OpenApiLicense
                {
                    Name = "Marcos Maximo",
                    Url = new Uri("https://github.com/marcosvmaximo")
                }
            });
            
            var documentationFileName = configuration.GetValue<string>("XmlDocumentationName");
            var filePath = Path.Combine(AppContext.BaseDirectory, documentationFileName);

            opt.IncludeXmlComments(filePath);
        });
        
        return services;
    }
}
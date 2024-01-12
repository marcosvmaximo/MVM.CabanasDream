using Asp.Versioning;

namespace MVM.CabanasDream.Festas.API.Configurations.Services;

public static class LoggingConfig
{
    public static IServiceCollection AddLoggingConfig(this IServiceCollection services)
    {
        services.AddApiVersioning(p =>
        {
            p.DefaultApiVersion = new ApiVersion(1, 0);
            p.ReportApiVersions = true;
            p.AssumeDefaultVersionWhenUnspecified = true;
        }).AddApiExplorer(options =>
        {
            // Agrupar por numero de versao
            options.GroupNameFormat = "'v'VVV";
            // Necessario para o correto funcionamento das rotas
            options.SubstituteApiVersionInUrl = true;
        }).EnableApiVersionBinding();

        return services;
    }
}
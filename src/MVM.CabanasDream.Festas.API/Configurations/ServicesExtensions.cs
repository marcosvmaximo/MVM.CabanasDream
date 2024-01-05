using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Festas.API.Configurations.DependencyInjection;
using MVM.CabanasDream.Festas.API.Configurations.Services;
using MVM.CabanasDream.Festas.Application.Commands;

namespace MVM.CabanasDream.Festas.API.Configurations;

public static class ServicesExtensions
{
    public static IServiceCollection AddServicesExtensions(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(new []{typeof(CriarFestaCommand).Assembly, typeof(MessageBus).Assembly});
        });
        
        services.AddDependencyInjection();
        
        services.AddIdentityConfig();
        services.AddVersioningConfig();
        services.AddLoggingConfig();
        services.AddHealthCheckConfig();
        
        services.AddSwaggerConfig();

        return services;
    }
}
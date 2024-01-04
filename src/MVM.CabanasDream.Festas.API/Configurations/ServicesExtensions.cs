using System.Reflection;
using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Festas.API.Configurations.Services;
using MVM.CabanasDream.Festas.Data.Context;
using MVM.CabanasDream.Festas.Data.Repositories;
using MVM.CabanasDream.Festas.Domain.Interfaces;

namespace MVM.CabanasDream.Festas.API.Configurations;

public static class ServicesExtensions
{
    public static IServiceCollection AddServicesExtensions(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(MediatorHandler).Assembly));

        services.AddScoped<DataContext>();
        services.AddScoped<INotificationHandler, NotificationHandler>();
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        services.AddTransient<IFestaRepository, FestaRepository>();
        
        services.AddSwaggerConfig();

        return services;
    }
}
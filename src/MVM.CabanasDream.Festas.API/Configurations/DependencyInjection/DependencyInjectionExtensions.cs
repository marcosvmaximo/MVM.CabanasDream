using System.Reflection;
using MediatR;
using MVM.CabanasDream.Core.Application;
using MVM.CabanasDream.Core.Bus;
using MVM.CabanasDream.Core.Messages;
using MVM.CabanasDream.Festas.API.Configurations.Services;
using MVM.CabanasDream.Festas.Application.Commands;
using MVM.CabanasDream.Festas.Data.Context;
using MVM.CabanasDream.Festas.Data.Repositories;
using MVM.CabanasDream.Festas.Domain.Interfaces;

namespace MVM.CabanasDream.Festas.API.Configurations.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        // Notification
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Bus
        services.AddScoped<IMessageBus, MessageBus>();
        
        // Repositories
        services.AddTransient<IFestaRepository, FestaRepository>();
        
        return services;
    }
}
using CustomMediatR.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MediatRLibrary;

public static class ConfigureService
{
    public static IServiceCollection AddMediatR(this IServiceCollection services, Assembly assembly)
    {
        Memory.assembly = assembly.FullName;
        services.AddScoped<IMediator, Mediator>();
        return services;
    }
}
public class Memory
{
    public static string assembly;
}

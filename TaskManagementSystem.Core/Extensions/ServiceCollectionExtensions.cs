using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Core.Mappings;

namespace TaskManagementSystem.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCoreModule(this IServiceCollection services)
    {
        services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        services.AddAutoMapper();
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<UserProfile>();
        }, typeof(ServiceCollectionExtensions).Assembly);
    }
}
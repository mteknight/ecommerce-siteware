using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Domain.Tests.Configurations;

public static class ServiceCollectionExtensions
{
    [UsedImplicitly]
    public static IServiceCollection RegisterDomainTestDependencies(this IServiceCollection services)
    {
        return services
            .RegisterDomainDependencies();
    }
}

public static class ServiceCollectionExtensions2
{
    [UsedImplicitly]
    public static IServiceCollection RegisterDomainDependencies(this IServiceCollection services)
    {
        return services
            .AddSingleton<IProductServiceFactory, ProductServiceFactory>();
    }
}

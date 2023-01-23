using Ecommerce.Common.Domain;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Domain.Configurations;

public static class ServiceCollectionExtensions2
{
    [UsedImplicitly]
    public static IServiceCollection RegisterDomainDependencies(this IServiceCollection services)
    {
        return services
            .AddSingleton(typeof(IAggregateWriterServiceFactory<,>), typeof(AggregateWriterServiceFactory<,>))
            .AddSingleton<IProductServiceFactory, ProductServiceFactory>();
    }
}

using Ecommerce.Common.Domain;
using Ecommerce.Data.Configurations;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Domain.Configurations;

public static class ServiceCollectionExtensions
{
    [UsedImplicitly]
    public static IServiceCollection RegisterDomainDependencies(this IServiceCollection services)
    {
        return services
            .RegisterDataDependencies()
            .AddSingleton(typeof(IAggregateWriterServiceFactory<,>), typeof(AggregateWriterServiceFactory<,>))
            .AddSingleton<IProductServiceFactory, ProductServiceFactory>();
    }
}

using Ecommerce.Domain.Configurations;

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

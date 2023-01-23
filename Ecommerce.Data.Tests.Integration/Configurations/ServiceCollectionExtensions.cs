using Ecommerce.Data.Tests.Configurations;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Data.Tests.Integration.Configurations;

public static class ServiceCollectionExtensions
{
    [UsedImplicitly]
    public static IServiceCollection RegisterDataIntegrationTestDependencies(this IServiceCollection services)
    {
        return services
            .RegisterDataTestDependencies();
    }
}

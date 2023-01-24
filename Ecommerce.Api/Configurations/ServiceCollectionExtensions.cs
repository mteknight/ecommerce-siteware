using Ecommerce.Common.Domain;
using Ecommerce.Data.Configurations;
using Ecommerce.Domain;
using Ecommerce.Domain.Configurations;

using JetBrains.Annotations;

namespace Ecommerce.Api.Configurations;

public static class ServiceCollectionExtensions
{
    [UsedImplicitly]
    public static IServiceCollection RegisterApiDependencies(this IServiceCollection services)
    {
        return services
            .RegisterDomainDependencies();
    }
}

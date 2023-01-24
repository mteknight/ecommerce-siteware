using Ecommerce.Data;
using Ecommerce.Data.Configurations;
using Ecommerce.Domain.Tests.Configurations;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Api.Tests.Configurations;

public static class ServiceCollectionExtensions
{
    [UsedImplicitly]
    public static IServiceCollection RegisterApiTestDependencies(this IServiceCollection services)
    {
        return services
            .RegisterDomainTestDependencies();
    }
}

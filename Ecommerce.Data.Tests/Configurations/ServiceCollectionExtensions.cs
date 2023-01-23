using Ecommerce.Data.Configurations;
using Ecommerce.Domain.Tests.Configurations;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Data.Tests.Configurations;

public static class ServiceCollectionExtensions
{
    [UsedImplicitly]
    public static IServiceCollection RegisterDataTestDependencies(this IServiceCollection services)
    {
        var options = new DbContextOptionsBuilder<InMemoryDbContext>()
            .UseInMemoryDatabase(databaseName: "EcommerceDb")
            .Options;
        
        return services
            .AddSingleton(typeof(DbContextOptions), options)
            .RegisterDataDependencies()
            .RegisterDomainTestDependencies();
    }
}

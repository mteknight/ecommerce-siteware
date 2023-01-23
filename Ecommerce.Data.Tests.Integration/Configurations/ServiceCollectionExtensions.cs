using Ecommerce.Domain.Tests.Configurations;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Data.Tests.Integration.Configurations;

public static class ServiceCollectionExtensions
{
    [UsedImplicitly]
    public static IServiceCollection RegisterDataTestDependencies(this IServiceCollection services)
    {
        return services
            .RegisterDataDependencies()
            .RegisterDomainTestDependencies();
    }
}

public static class ServiceCollectionExtensions2
{
    [UsedImplicitly]
    public static IServiceCollection RegisterDataDependencies(this IServiceCollection services)
    {
        var options = new DbContextOptionsBuilder<InMemoryDbContext>()
            .UseInMemoryDatabase(databaseName: "EcommerceDb")
            .Options;

        var context = new InMemoryDbContext(options);
        
        return services
            .AddSingleton(context)
            .AddSingleton(typeof(IAggregateDataReaderService<>), typeof(AggregateDataReaderService<>))
            .AddSingleton(typeof(IAggregateDataWriterService<,>), typeof(AggregateDataWriterService<,>));
    }
}

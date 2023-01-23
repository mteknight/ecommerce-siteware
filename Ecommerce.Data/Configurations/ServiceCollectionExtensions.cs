using Ecommerce.Common.Data;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Data.Configurations;

public static class ServiceCollectionExtensions
{
    [UsedImplicitly]
    public static IServiceCollection RegisterDataDependencies(this IServiceCollection services)
    {
        var options = new DbContextOptionsBuilder<InMemoryDbContext>()
            .UseInMemoryDatabase(databaseName: "EcommerceDb")
            .Options;

        var context = new InMemoryDbContext(options);
        
        return services
            .AddSingleton(typeof(DbContext), context)
            .AddSingleton(typeof(IAggregateDataReaderService<>), typeof(AggregateDataReaderService<>))
            .AddSingleton(typeof(IAggregateDataWriterService<,>), typeof(AggregateDataWriterService<,>));
    }
}

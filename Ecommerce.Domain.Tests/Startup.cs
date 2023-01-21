using Ecommerce.Domain.Tests.Configurations;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Domain.Tests
{
    [UsedImplicitly]
    public class Startup
    {
        public virtual void ConfigureServices(IServiceCollection services) => services.RegisterDomainTestDependencies();
    }
}

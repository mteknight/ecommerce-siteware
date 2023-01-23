using Ecommerce.Data.Tests.Integration.Configurations;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Data.Tests.Integration
{
    [UsedImplicitly]
    public class Startup
    {
        public virtual void ConfigureServices(IServiceCollection services) => services.RegisterDataIntegrationTestDependencies();
    }
}

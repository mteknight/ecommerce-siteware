using Ecommerce.Data.Tests.Configurations;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Data.Tests
{
    [UsedImplicitly]
    public class Startup
    {
        public virtual void ConfigureServices(IServiceCollection services) => services.RegisterDataTestDependencies();
    }
}

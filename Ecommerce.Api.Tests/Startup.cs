using Ecommerce.Api.Tests.Configurations;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Api.Tests
{
    [UsedImplicitly]
    public class Startup
    {
        public virtual void ConfigureServices(IServiceCollection services) => services.RegisterApiTestDependencies();
    }
}

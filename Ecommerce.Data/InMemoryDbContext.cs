using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data;

public class InMemoryDbContext : DbContext
{
    public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
        : base(options)
    {
    }

    public InMemoryDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Not happy with this hardcoded but will suffice for now!
        const string domainAssembly = "Ecommerce.Domain";
        var assembly = Assembly.Load(new AssemblyName(domainAssembly));
        
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}

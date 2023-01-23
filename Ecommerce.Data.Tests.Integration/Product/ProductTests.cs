using System;
using System.Reflection;

using Dawn;

using Ecommerce.Common.Domain;
using Ecommerce.Domain;
using Ecommerce.Domain.Tests.Product;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Ecommerce.Data.Tests.Integration;

public sealed record ProductTests
{
    private readonly IAggregateDataWriterService<Product, ProductValidated> sut;

    public ProductTests(IAggregateDataWriterService<Product, ProductValidated> sut)
    {
        this.sut = Guard.Argument(sut, nameof(sut)).NotNull().Value;
    }

    [Theory]
    [MemberData(nameof(ProductTestData.ValidProductTestData), MemberType = typeof(ProductTestData))]
    public void GivenValidProduct_WhenAddingNew_ThenExpectResultTrueAndReadProduct(Product product)
    {
        // Arrange
        var productValidated = new ProductValidated(product);

        // Act
        var wasAdded = this.sut.Add(productValidated);
        var newProduct = this.sut.Get(product.Id);

        // Assert
        wasAdded.Should().BeTrue("The new product is expected to be added successfully.");
        newProduct.Should().NotBeNull("The new product was added successfully and should be retrievable.");
    }
}

public interface IAggregateDataReaderService<out TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
{
    TAggregateRoot? Get(Guid productId);
}

public record AggregateDataReaderService<TAggregateRoot> : IAggregateDataReaderService<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
{
    public AggregateDataReaderService(InMemoryDbContext context)
    {
        this.Context = Guard.Argument(context, nameof(context)).NotNull().Value;
    }

    protected InMemoryDbContext Context { get; }

    public virtual TAggregateRoot? Get(Guid productId)
    {
        return this.Context.Find<TAggregateRoot>(productId);
    }
}

public interface IAggregateDataWriterService<out TAggregateRoot, in TAggregateValidated>
    : IAggregateDataReaderService<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    bool Add(TAggregateValidated productValidated);
}

public record AggregateDataWriterService<TAggregateRoot, TAggregateValidated> 
    : AggregateDataReaderService<TAggregateRoot>, IAggregateDataWriterService<TAggregateRoot, TAggregateValidated>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    public AggregateDataWriterService(InMemoryDbContext context)
        : base(context)
    {
    }

    public virtual bool Add(TAggregateValidated productValidated)
    {
        this.Context.Add<TAggregateRoot>(productValidated);

        return this.Context.SaveChanges() > 0;
    }
}

public sealed class InMemoryDbContext : DbContext
{
    public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) 
        : base(options) 
    { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var type = typeof(Ecommerce.Domain.Configurations.ServiceCollectionExtensions);
        var configurationsAssembly = Assembly.GetAssembly(type);
        
        modelBuilder.ApplyConfigurationsFromAssembly(configurationsAssembly!);
    }
}

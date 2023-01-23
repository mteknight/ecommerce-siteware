using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public sealed record Product : IAggregateRoot<Product>
{
    public Product(
        string name,
        decimal price)
    {
        this.Name = name;
        this.Price = price;
    }

    public Guid Id { get; set; } = Guid.Empty;

    public string Name { get; init; }

    public decimal Price { get; init; }
}

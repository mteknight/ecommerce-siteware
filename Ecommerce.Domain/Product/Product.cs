using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public sealed record Product : IAggregateRoot<Product>
{
    public Product(
        string name,
        double price)
    {
        this.Name = name;
        this.Price = price;
    }

    public Guid Id { get; set; } = Guid.Empty;

    public string Name { get; init; }

    public double Price { get; init; }
}

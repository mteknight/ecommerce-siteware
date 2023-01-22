using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public sealed record ProductValidated : AggregateValidated<Product>
{
    public ProductValidated(Product aggregate) 
        : base(aggregate)
    {
    }

    public override bool Validate()
    {
        return !string.IsNullOrWhiteSpace(this.Aggregate?.Name) &&
               !double.IsPositiveInfinity(this.Aggregate.Price) &&
               this.Aggregate.Price >= 0;
    }
}

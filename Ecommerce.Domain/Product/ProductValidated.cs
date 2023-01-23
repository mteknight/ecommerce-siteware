using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public sealed class ProductValidated : AggregateValidated<Product>
{
    internal ProductValidated(Product aggregate) 
        : base(aggregate)
    {
    }

    protected override bool Validate(Product? aggregate)
    {
        return !string.IsNullOrWhiteSpace(aggregate?.Name) &&
               aggregate.Price >= 0;
    }
}

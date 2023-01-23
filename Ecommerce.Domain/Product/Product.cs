using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public sealed record Product : IAggregateRoot<Product>
{
    private IPromotion promotion = new NoPromotion();

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

    public IPromotion Promotion
    {
        get => this.promotion;
        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        set => this.promotion = value ?? new NoPromotion();
    }
}

public interface IPromotion
{
    decimal CalculatePrice(
        int quantity, 
        decimal unitPrice);
}

public sealed record NoPromotion : IPromotion 
{
    public decimal CalculatePrice(
        int quantity, 
        decimal unitPrice)  
    {
        throw new NotImplementedException();
    }

    public override string ToString() => string.Empty;
}

public sealed record TwoForOne : IPromotion 
{
    public decimal CalculatePrice(
        int quantity, 
        decimal unitPrice) 
    {
        throw new NotImplementedException();
    }

    public override string ToString() => "Leve 2, Pague 1";
}

public sealed record ThreeForTen : IPromotion {
    public decimal CalculatePrice(
        int quantity, 
        decimal unitPrice)
    {
        throw new NotImplementedException();
    }

    public override string ToString() => "3 por 10 reais";
}

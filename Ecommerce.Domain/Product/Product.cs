using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public sealed record Product : IAggregateRoot<Product>
{
    private readonly IPromotion promotion = new NoPromotion();

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

    public IPromotion.PromotionType PromotionType
    {
        get => IPromotion.Get(this.promotion);
        set => this.Promotion = IPromotion.Create(value);
    }

    public IPromotion Promotion { get; private set; } = new NoPromotion();
}

public interface IPromotion
{
    public enum PromotionType
    {
        NoPromotion = 0,
        TwoForOne,
        ThreeForTen
    }

    private static readonly Dictionary<PromotionType, Func<IPromotion>> Promotions =
        new() 
        {
            { PromotionType.NoPromotion, () => new NoPromotion() },
            { PromotionType.TwoForOne, () => new TwoForOne() },
            { PromotionType.ThreeForTen, () => new ThreeForTen() }
        };

    decimal CalculatePrice(
        int quantity, 
        decimal unitPrice);

    internal static IPromotion Create(PromotionType type)
    {
        return Promotions.TryGetValue(type, out var creator) 
            ? creator.Invoke() 
            : new NoPromotion();
    }

    internal static PromotionType Get(IPromotion promotion)
    {
        var name = promotion.GetType().Name;
        return (PromotionType)Enum.Parse(typeof(PromotionType), name);
    }
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

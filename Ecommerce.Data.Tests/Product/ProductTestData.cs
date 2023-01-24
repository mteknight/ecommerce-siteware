using AutoFixture;

using Ecommerce.Common.Extensions;
using Ecommerce.Common.Tests;
using Ecommerce.Domain;

namespace Ecommerce.Data.Tests;

public record ProductTestData
{
    public static IEnumerable<object[]> ValidProductTestData()
    {
        var fixture = new Fixture();

        return TestData
                .NewSet(new Product(fixture.Create<string>(), fixture.Create<decimal>()) { PromotionType = IPromotion.PromotionType.NoPromotion })
                .NewSet(new Product(fixture.Create<string>(), fixture.Create<decimal>()) { PromotionType = IPromotion.PromotionType.NoPromotion })
                .NewSet(new Product(fixture.Create<string>(), fixture.Create<decimal>()) { PromotionType = IPromotion.PromotionType.NoPromotion })
            ;
    }
}

using AutoFixture;

using Ecommerce.Common.Extensions;
using Ecommerce.Common.Tests;

namespace Ecommerce.Domain.Tests;

public sealed record ProductTestData
{
    public static IEnumerable<object[]> InvalidProductTestData()
    {
        var fixture = new Fixture();

        return TestData
                .NewSet(new Product(default!, fixture.Create<decimal>()))
                .NewSet(new Product(string.Empty, fixture.Create<decimal>()))
                .NewSet(new Product(StringExtensions.Whitespace, fixture.Create<decimal>()))
                .NewSet(new Product(fixture.Create<string>(), -1))
                .NewSet(new Product(fixture.Create<string>(), -43.765m))
                .NewSet(new Product(fixture.Create<string>(), decimal.MinValue))
            ;
    }

    public static IEnumerable<object[]> ProductWithoutPromotionTestData()
    {
        var fixture = new Fixture();

        return TestData
                .NewSet(new Product(fixture.Create<string>(), fixture.Create<decimal>()) { Id = fixture.Create<Guid>() })
            ;
    }
}

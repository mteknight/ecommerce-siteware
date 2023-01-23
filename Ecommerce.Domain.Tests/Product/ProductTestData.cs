﻿using AutoFixture;

using Ecommerce.Common.Extensions;
using Ecommerce.Common.Tests;

namespace Ecommerce.Domain.Tests.Product;

internal sealed record ProductTestData
{
    public static IEnumerable<object[]> InvalidProductTestData()
    {
        var fixture = new Fixture();

        return TestData
                .NewSet(new Domain.Product(default!, fixture.Create<decimal>()))
                .NewSet(new Domain.Product(string.Empty, fixture.Create<decimal>()))
                .NewSet(new Domain.Product(StringExtensions.Whitespace, fixture.Create<decimal>()))
                .NewSet(new Domain.Product(fixture.Create<string>(), -1))
                .NewSet(new Domain.Product(fixture.Create<string>(), -43.765m))
                .NewSet(new Domain.Product(fixture.Create<string>(), decimal.MinValue))
            ;
    }

    public static IEnumerable<object[]> ValidProductTestData()
    {
        var fixture = new Fixture();

        return TestData
                .NewSet(new Domain.Product(fixture.Create<string>(), fixture.Create<decimal>()) { Promotion = new NoPromotion() })
                .NewSet(new Domain.Product(fixture.Create<string>(), fixture.Create<decimal>()) { Promotion = new TwoForOne() })
                .NewSet(new Domain.Product(fixture.Create<string>(), fixture.Create<decimal>()) { Promotion = new ThreeForTen() })
            ;
    }

    public static IEnumerable<object[]> ProductWithoutPromotionTestData()
    {
        var fixture = new Fixture();

        return TestData
                .NewSet(new Domain.Product(fixture.Create<string>(), fixture.Create<decimal>()) { Promotion = default })
            ;
    }
}

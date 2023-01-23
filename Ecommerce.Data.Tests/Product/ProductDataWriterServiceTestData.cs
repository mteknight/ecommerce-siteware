using AutoFixture;

using Ecommerce.Common.Tests;
using Ecommerce.Domain;

namespace Ecommerce.Data.Tests;

public sealed record ProductDataWriterServiceTestData
{
    public static IEnumerable<object[]> ValidProductTestData()
    {
        var fixture = new Fixture();
        var product = fixture.Create<Product>();
        var productValidated = new ProductValidated(product);

        return TestData
            .NewSet(product, productValidated, fixture);
    }
}
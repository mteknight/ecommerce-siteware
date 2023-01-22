using Ecommerce.Common.Extensions;
using Ecommerce.Common.Tests;


namespace Ecommerce.Domain.Tests.Product;

internal sealed record ProductTestData
{
    public static IEnumerable<object[]> InvalidProductTestData()
    {
        return TestData
            .NewSet(new Domain.Product(default, 0)) 
            .NewSet(new Domain.Product(string.Empty, 0))
            .NewSet(new Domain.Product(StringExtensions.Whitespace, 0));
                
    }
}

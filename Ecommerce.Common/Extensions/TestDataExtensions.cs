using System.Diagnostics.Contracts;

using Ecommerce.Common.Tests;

namespace Ecommerce.Common.Extensions;

public static class TestDataExtensions
{
    [Pure]
    public static TestData NewSet(
        this TestData testData,
        params object[] dataSet)
    {
        testData.Dataset = testData.Dataset.Append(dataSet);

        return testData;
    }
}
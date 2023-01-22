using System.Collections;
using System.Diagnostics.Contracts;

namespace Ecommerce.Common.Tests;

public sealed record TestData : IEnumerable<object[]>
{
    private TestData(params object[] dataSet)
    {
        this.Dataset = this.Dataset.Append(dataSet);
    }

    internal IEnumerable<object[]> Dataset { get; set; } = ArraySegment<object[]>.Empty;

    public IEnumerator<object[]> GetEnumerator()
    {
        return this.Dataset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    [Pure]
    public static TestData NewSet(params object[] dataSet) => new(dataSet);
}

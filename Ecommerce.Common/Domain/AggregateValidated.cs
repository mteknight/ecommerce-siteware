using System.Diagnostics.Contracts;

namespace Ecommerce.Common.Domain;

public abstract class AggregateValidated<TAggregate> : IValidate<TAggregate>
    where TAggregate : class, IAggregate<TAggregate>
{
    private readonly TAggregate aggregate;

    protected AggregateValidated(TAggregate aggregate)
    {
        // ReSharper disable once VirtualMemberCallInConstructor => Made safe.
        this.IsValid = this.Validate(aggregate);
        this.aggregate = aggregate;
    }

    public bool IsValid { get; }

    public static implicit operator TAggregate(AggregateValidated<TAggregate> validatedAggregate)
    {
        return validatedAggregate.aggregate;
    }

    [Pure]
    public bool Validate() => this.Validate(this.aggregate);

    [Pure]
    protected abstract bool Validate(TAggregate? aggregate);
}

namespace Ecommerce.Common.Domain;

public abstract record AggregateValidated<TAggregate> : IValidate<TAggregate>
    where TAggregate : class, IAggregate<TAggregate>
{
    protected AggregateValidated(TAggregate aggregate)
    {
        this.Set(aggregate);
    }

    protected TAggregate? Aggregate { get; private set; }

    public bool IsValid { get; private set; }

    public static implicit operator TAggregate(AggregateValidated<TAggregate> validatedAggregate)
    {
        return validatedAggregate.Aggregate!;
    }

    public abstract bool Validate();

    public void Set(TAggregate aggregate)
    {
        this.Aggregate = aggregate;
        this.IsValid = this.Validate();
    }
}
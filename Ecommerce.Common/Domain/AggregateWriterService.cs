using Dawn;

using JetBrains.Annotations;

namespace Ecommerce.Common.Domain;

[UsedImplicitly]
public sealed record AggregateWriterService<TAggregateRoot, TAggregateValidated>
    : IAggregateWriterService<TAggregateRoot, TAggregateValidated>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    public AggregateWriterService(TAggregateValidated validatedAggregate)
    {
        this.ValidatedAggregate = Guard.Argument(validatedAggregate, nameof(validatedAggregate)).NotNull().Value;
    }

    public TAggregateValidated ValidatedAggregate { get; }

    public Guid Save()
    {
        if (!this.ValidatedAggregate.IsValid)
        {
            return Guid.Empty;
        }

        return Guid.NewGuid();
    }
}

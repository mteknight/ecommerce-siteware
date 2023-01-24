using Dawn;

using Ecommerce.Common.Data;

using JetBrains.Annotations;

namespace Ecommerce.Common.Domain;

[UsedImplicitly]
public record AggregateWriterService<TAggregateRoot, TAggregateValidated>
    : IAggregateWriterService<TAggregateRoot, TAggregateValidated>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    private readonly IAggregateDataWriterService<TAggregateRoot, TAggregateValidated> dataWriterService;

    public AggregateWriterService(
        TAggregateValidated validatedAggregate,
        IAggregateDataWriterService<TAggregateRoot, TAggregateValidated> dataWriterService)
    {
        this.ValidatedAggregate = Guard.Argument(validatedAggregate, nameof(validatedAggregate)).NotNull().Value;
        this.dataWriterService = Guard.Argument(dataWriterService, nameof(dataWriterService)).NotNull().Value;
    }

    public TAggregateValidated ValidatedAggregate { get; }

    public virtual bool Save()
    {
        return this.ValidatedAggregate.IsValid &&
               this.dataWriterService.Add(this.ValidatedAggregate);
    }
}

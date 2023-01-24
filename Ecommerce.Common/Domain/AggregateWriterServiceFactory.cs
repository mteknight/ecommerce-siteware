using Dawn;

using Ecommerce.Common.Data;

using JetBrains.Annotations;

namespace Ecommerce.Common.Domain;

[UsedImplicitly]
public record AggregateWriterServiceFactory<TAggregateRoot, TAggregateValidated>
    : IAggregateWriterServiceFactory<TAggregateRoot, TAggregateValidated>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    private readonly IAggregateDataWriterService<TAggregateRoot, TAggregateValidated> dataWriterService;

    public AggregateWriterServiceFactory(IAggregateDataWriterService<TAggregateRoot, TAggregateValidated> dataWriterService)
    {
        this.dataWriterService = Guard.Argument(dataWriterService, nameof(dataWriterService)).NotNull().Value;
    }

    public virtual IAggregateWriterService<TAggregateRoot, TAggregateValidated> Create(TAggregateValidated validatedAggregate)
    {
        return new AggregateWriterService<TAggregateRoot, TAggregateValidated>(validatedAggregate, this.dataWriterService);
    }
}

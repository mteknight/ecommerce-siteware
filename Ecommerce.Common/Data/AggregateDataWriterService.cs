using Dawn;

using Ecommerce.Common.Domain;

using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Common.Data;

public record AggregateDataWriterService<TAggregateRoot, TAggregateValidated>
    : IAggregateDataWriterService<TAggregateRoot, TAggregateValidated>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    private readonly DbContext context;
    private readonly IAggregateDataReaderService<TAggregateRoot> readerService;

    public AggregateDataWriterService(
        DbContext context,
        IAggregateDataReaderService<TAggregateRoot> readerService)
    {
        this.context = Guard.Argument(context, nameof(context)).NotNull().Value;
        this.readerService = Guard.Argument(readerService, nameof(readerService)).NotNull().Value;
    }

    public virtual async Task<bool> Add(
        TAggregateValidated productValidated,
        CancellationToken cancellationToken = default)
    {
        await this.context.AddAsync<TAggregateRoot>(productValidated, cancellationToken);
        var changes = await this.context.SaveChangesAsync(cancellationToken);

        return changes > 0;
    }

    public virtual ValueTask<TAggregateRoot?> Get(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        return this.readerService.Get(productId, cancellationToken);
    }
}

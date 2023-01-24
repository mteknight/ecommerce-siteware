using Dawn;

using Ecommerce.Common.Domain;

using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Common.Data;

public record AggregateDataReaderService<TAggregateRoot> : IAggregateDataReaderService<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
{
    private readonly DbContext context;

    public AggregateDataReaderService(DbContext context)
    {
        this.context = Guard.Argument(context, nameof(context)).NotNull().Value;
    }

    public virtual ValueTask<TAggregateRoot?> Get(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        return this.context.FindAsync<TAggregateRoot>(productId, cancellationToken);
    }
}

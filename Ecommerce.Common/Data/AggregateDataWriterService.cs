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

    public virtual bool Add(TAggregateValidated productValidated)
    {
        this.context.Add<TAggregateRoot>(productValidated);

        return this.context.SaveChanges() > 0;
    }

    public virtual TAggregateRoot? Get(Guid productId) => this.readerService.Get(productId);
}
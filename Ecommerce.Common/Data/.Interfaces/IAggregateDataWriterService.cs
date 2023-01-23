using Ecommerce.Common.Domain;

namespace Ecommerce.Common.Data;

public interface IAggregateDataWriterService<out TAggregateRoot, in TAggregateValidated>
    : IAggregateDataReaderService<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
    where TAggregateValidated : AggregateValidated<TAggregateRoot>
{
    bool Add(TAggregateValidated productValidated);
}
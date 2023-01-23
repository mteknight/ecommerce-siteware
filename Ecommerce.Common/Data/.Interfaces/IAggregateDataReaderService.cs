using Ecommerce.Common.Domain;

namespace Ecommerce.Common.Data;

public interface IAggregateDataReaderService<out TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TAggregateRoot>
{
    TAggregateRoot? Get(Guid productId);
}

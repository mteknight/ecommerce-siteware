namespace Ecommerce.Common.Domain;

public interface IValidate<in TAggregate>
    where TAggregate : class, IAggregate<TAggregate>
{
    bool Validate();
}

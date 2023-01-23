using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public interface IProductServiceFactory : IAggregateServiceFactory<Product, ProductValidated>
{}

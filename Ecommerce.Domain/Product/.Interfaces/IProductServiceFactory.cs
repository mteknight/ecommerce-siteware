using Ecommerce.Common.Domain;

namespace Ecommerce.Domain;

public interface IProductServiceFactory : IAggregateRootServiceFactory<IProductService, Product>
{}

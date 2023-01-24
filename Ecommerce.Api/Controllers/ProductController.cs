using Dawn;

using Ecommerce.Domain;

using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductServiceFactory productServiceFactory;

    public ProductController(IProductServiceFactory productServiceFactory)
    {
        this.productServiceFactory = Guard.Argument(productServiceFactory, nameof(productServiceFactory)).NotNull().Value;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Add(
        [FromBody] Product? product,
        CancellationToken cancellationToken = default)
    {
        if (product is null)
        {
            return this.BadRequest();
        }

        var service = this.productServiceFactory.Create(product);
        var id = await service.Save(cancellationToken);

        return this.Ok(id);
    }
}

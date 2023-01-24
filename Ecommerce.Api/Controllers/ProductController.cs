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
    public ActionResult<Guid> Add([FromBody] Product? product)
    {
        if (product is null)
        {
            return this.BadRequest();
        }

        var service = this.productServiceFactory.Create(product);
        var id = service.Save();

        return this.Ok(id);
    }
}

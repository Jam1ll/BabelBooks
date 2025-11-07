using Asp.Versioning;
using BabelBooks.Core.Application.Features.ProductsCQRS.Commands.Create;
using BabelBooks.Core.Application.Features.ProductsCQRS.Commands.UpdatePrice;
using BabelBooks.Core.Application.Features.ProductsCQRS.Queries.Get;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BabelBooksAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Endpoints for product manipulation")]
    public class ProductController : BaseAPIController
    {
        //
        // GET: api/product/{productId}
        //
        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid productId)
        {
            var query = new GetProductByIdQuery(productId);

            var product = await Mediator.Send(query); //usar la propiedad heredada de Mediator

            return product is not null //operador ternario para verificar si es nulo o no
                ? Ok(product) 
                : NotFound();
        }

        //
        // POST: api/product
        //
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var productId = await Mediator.Send(command);

            return CreatedAtAction
                (nameof(GetById),
                new { id = productId },
                new { id = productId });
        }

        //
        // PUT: api/product/{id}/price
        //
        [HttpPut("{productId}/price")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePrice(Guid productId, [FromBody] decimal productNewPrice)
        {
            var command = new UpdateProductCommand(productId, productNewPrice);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}

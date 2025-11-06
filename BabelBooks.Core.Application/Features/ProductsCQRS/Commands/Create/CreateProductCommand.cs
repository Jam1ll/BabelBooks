using MediatR;

namespace BabelBooks.Core.Application.Features.ProductsCQRS.Commands.Create
{
    //un comando es un DTO que representa la intencion de hacer algo. En este caso, crear un producto.
    public record CreateProductCommand(
        string ProductName,
        decimal ProductPrice
        ) : IRequest<Guid>;
}

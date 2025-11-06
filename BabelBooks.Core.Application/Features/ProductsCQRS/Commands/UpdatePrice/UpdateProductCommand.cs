using MediatR;

namespace BabelBooks.Core.Application.Features.ProductsCQRS.Commands.UpdatePrice
{
    //este comando no retorna nada (a diferencia del Create),
    //solo indica la intencion de actualizar el precio de un producto.
    public record UpdateProductCommand(
        Guid ProductId,
        decimal ProductNewPrice
        ) : IRequest;
}

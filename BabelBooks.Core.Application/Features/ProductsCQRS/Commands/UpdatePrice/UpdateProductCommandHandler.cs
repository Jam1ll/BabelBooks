using BabelBooks.Core.Domain.Aggregates;
using Marten;
using MediatR;

namespace BabelBooks.Core.Application.Features.ProductsCQRS.Commands.UpdatePrice
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IDocumentSession _documentSession;

        public UpdateProductCommandHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // cargar el stream de eventos del producto a actualizar.
            // Marten lee todos los eventos del agregado (CreateProduct, UpdateProduct...) y lo reconstruye,
            // de forma que se obtiene el estado ACTUAL. Esto es EVENT SOURCING.
            var product = await _documentSession.Events
                .AggregateStreamAsync<Product>(request.ProductId, token: cancellationToken)
                //if (product == null)
                ?? throw new Exception($"Product with Id {request.ProductId} not found.");

            //ejecutar logica de negocio para actualizar el precio
            product.UpdateProductPrice(request.ProductNewPrice);

            //guardar los nuevos eventos generados en el stream del producto
            var events = product.GetUncommitedEvents();
            _documentSession.Events.Append(product.Id, events);

            //guardar cambios en la base de datos
            await _documentSession.SaveChangesAsync(cancellationToken);
        }
    }
}

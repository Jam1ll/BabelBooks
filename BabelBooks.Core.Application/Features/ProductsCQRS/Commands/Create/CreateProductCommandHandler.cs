using BabelBooks.Core.Domain.Aggregates;
using Marten;
using MediatR;

namespace BabelBooks.Core.Application.Features.ProductsCQRS.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        //IDocumentSession de Marten para interactuar con los agregados/eventos en la base de datos
        private readonly IDocumentSession _documentSession;

        public CreateProductCommandHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        //implementando interfaz de IRequestHandler para manejar el comando CreateProductCommand
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //usar el metodo estatico factory para crear un nuevo producto
            var product = Product.Create(request.ProductName, request.ProductPrice);

            //guardar los eventos generados con Marten. Esto genera un nuevo 'Stream' de eventos para el agregado
            _documentSession.Events.StartStream<Product>(product.Id, product.GetUncommitedEvents());

            //guardar los cambios en la base de datos
            await _documentSession.SaveChangesAsync(cancellationToken);

            //retornar el Id del nuevo producto creado
            return product.Id;
        }
    }
}

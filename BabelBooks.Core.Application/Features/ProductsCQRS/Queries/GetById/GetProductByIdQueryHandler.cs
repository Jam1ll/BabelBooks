using BabelBooks.Core.Application.Features.ProductsCQRS.Queries.Get;
using BabelBooks.Core.Application.ReadModels.ProductReadModels;
using Marten;
using MediatR;

namespace BabelBooks.Core.Application.Features.ProductsCQRS.Queries.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductReadModel?>
    {
        private readonly IQuerySession _querySession;

        public GetProductByIdQueryHandler(IQuerySession querySession) => _querySession = querySession;

        public async Task<ProductReadModel?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            //consultar el modelo de lectura, no los eventos
            var readModel = await _querySession.LoadAsync<ProductReadModel>(request.ProductId, cancellationToken);

            return readModel;
        }
    }
}

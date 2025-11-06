using BabelBooks.Core.Application.ReadModels.ProductReadModels;
using MediatR;

namespace BabelBooks.Core.Application.Features.ProductsCQRS.Queries.Get
{
    public record GetProductByIdQuery( //pide un ProductReadModel, no un Aggregate
        Guid ProductId
        ) : IRequest<ProductReadModel?>; //puede retornar null
}

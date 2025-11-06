using BabelBooks.Core.Application.ReadModels.ProductReadModels;
using BabelBooks.Core.Domain.Events;
using Marten.Events.Aggregation;

namespace BabelBooks.Infrastructure.Persistence.Projections
{
    public class ProductProjection : SingleStreamProjection<ProductReadModel, Guid>
    {
        public void Apply(ProductCreatedEvent @event, ProductReadModel readModel)
        {
            readModel.Id = @event.ProductId;
            readModel.ProductName = @event.ProductName;
            readModel.ProductPrice = @event.ProductPrice;
        }

        //funcion de flecha. Puede ser una funcion normal también.
        public void Apply(ProductPriceUpdatedEvent @event, ProductReadModel readModel)
            => readModel.ProductPrice = @event.ProductNewPrice;
    }
}

namespace BabelBooks.Core.Domain.Events
{
    public record ProductCreatedEvent(
        Guid ProductId,
        string ProductName,
        decimal ProductPrice
        );

    public record ProductPriceUpdatedEvent(
        Guid ProductId,
        decimal ProductNewPrice
        );
}

using BabelBooks.Core.Domain.Events;
using BabelBooks.Core.Domain.Shared;

namespace BabelBooks.Core.Domain.Aggregates
{
    public class Product : AggregateBase
    {
        public string ProductName { get; private set; } = string.Empty;
        public decimal ProductPrice { get; private set; }
        public int ProductVersion { get; private set; }

        public Product() { } //constructor vacio para la hidratacion del agregado via Marten

        //
        // logica de negocio
        //

        //factory method para crear un nuevo producto
        public static Product Create(string productName, decimal productPrice)
        {
            //verificacion de reglas de negocio
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException($"Product name can't be empty");
            if (productPrice < 0)
                throw new ArgumentException($"Product price can't be negative");

            //crear instancia
            var product = new Product();

            //generar evento. Esto reemplaza el tipico this.ProductId = Guid.NewGuid();, etc
            product.RaiseEvent(new ProductCreatedEvent(Guid.NewGuid(), productName, productPrice));

            return product;
        }

        public void UpdateProductPrice(decimal productNewPrice)
        {
            if (productNewPrice <= 0)
                throw new ArgumentException("Price should be positive.");
            if (productNewPrice == ProductPrice)
                return; // no changes == no event

            //generar evento
            RaiseEvent(new ProductPriceUpdatedEvent(Id, productNewPrice));
        }

        //metodos ApplyEvent (mutadores) para actualizar el estado del agregado en respuesta a eventos

        public void ApplyEvent(ProductCreatedEvent @event)
        {
            Id = @event.ProductId;
            ProductName = @event.ProductName;
            ProductPrice = @event.ProductPrice;
            ProductVersion++;
        }

        public void ApplyEvent(ProductPriceUpdatedEvent @event)
        {
            ProductPrice = @event.ProductNewPrice;
            ProductVersion++;
        }
    }
}

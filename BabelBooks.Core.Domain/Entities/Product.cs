using BabelBooks.Core.Domain.Shared;

namespace BabelBooks.Core.Domain.Entities
{
    public class Product : AggregateBase
    {
        public string ProductName { get; private set; } = string.Empty;
        public decimal ProductPrice { get; private set; }
        public int ProductVersion { get; private set; }

        public Product() { } //constructor vacio para la hidratacion del agregado via Marten
    }
}
